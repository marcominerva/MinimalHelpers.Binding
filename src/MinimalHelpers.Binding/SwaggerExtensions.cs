using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MinimalHelpers.Binding;

/// <summary>
/// Provides an extension method for adding an Operation Filter that allows to correctly handle file input in Swagger.
/// </summary>
/// <remarks>Remember to call the <see cref="RouteHandlerBuilderExtensions.AcceptsFormFile(RouteHandlerBuilder)"/> 
/// or <see cref="RouteHandlerBuilderExtensions.AcceptsFormFileCollection(RouteHandlerBuilder)"/> methods
/// on the endpoint that requires a single or multiple file input.
/// </remarks>
/// <seealso cref="RouteHandlerBuilder"/>
/// <seealso cref="IOperationFilter"/>
public static class SwaggerExtensions
{
    /// <summary>
    /// Adds an Operation Filter that allows to correctly handle file input in Swagger.
    /// </summary>
    /// <remarks>Remember to call the <see cref="RouteHandlerBuilderExtensions.AcceptsFormFile(RouteHandlerBuilder)"/> 
    /// or <see cref="RouteHandlerBuilderExtensions.AcceptsFormFileCollection(RouteHandlerBuilder)"/> methods
    /// on the endpoint that requires a single or multiple file input.
    /// </remarks>
    /// <seealso cref="RouteHandlerBuilder"/>
    /// <seealso cref="IOperationFilter"/>
    public static void AddFormFile(this SwaggerGenOptions options)
        => options.OperationFilter<FormFileOperationFilter>();
}
