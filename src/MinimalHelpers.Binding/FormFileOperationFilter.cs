using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MinimalHelpers.Binding;

/// <summary>
/// An Operation Filter that allows to correctly handle file input in Swagger.
/// </summary>
/// <remarks>Remember to call the <see cref="OpenApiRouteHandlerBuilderExtensions.Accepts{TRequest}(RouteHandlerBuilder, string, string[])"/> method
/// on the endpoint that requires a single or multiple file input.
/// </remarks>
/// <seealso cref="IOperationFilter"/>
public class FormFileOperationFilter : IOperationFilter
{
    /// <inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var acceptsMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata.OfType<IAcceptsMetadata>();

        var acceptsFormFile = acceptsMetadata
            .Any(m => m.RequestType == typeof(IFormFile) || m.RequestType == typeof(FormFileContent));

        var acceptsFormFileCollection = acceptsMetadata
            .Any(m => m.RequestType == typeof(IFormFileCollection) || m.RequestType == typeof(FormFileContentCollection));

        if (acceptsFormFile || acceptsFormFileCollection)
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Required = new HashSet<string> { "file" },
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["file"] = acceptsFormFile ? new OpenApiSchema()
                                {
                                    Type = "string",
                                    Format = "binary"
                                } : new OpenApiSchema()
                                {
                                    Type = "array",
                                    Items = new OpenApiSchema
                                    {
                                        Type = "string",
                                        Format = "binary"
                                    }
                                }
                            }
                        },
                        Encoding = new Dictionary<string, OpenApiEncoding>
                        {
                            ["file"] = new OpenApiEncoding { Style = ParameterStyle.Form }
                        }
                    }
                }
            };
        }
    }
}
