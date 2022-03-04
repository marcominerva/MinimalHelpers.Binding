using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;

namespace MinimalHelpers.Binding;

/// <summary>
/// Provides extension methods for adding a <see cref="Endpoint.Metadata"/> that makes the endpoint able to receive a single or multiple file upload.
/// </summary>
/// <seealso cref="RouteHandlerBuilder"/>
/// <seealso cref="EndpointMetadataCollection"/>
public static class RouteHandlerBuilderExtensions
{
    /// <summary>
    /// Adds a <see cref="IAcceptsMetadata"/> to <see cref="EndpointBuilder.Metadata"/> for the <see cref="IFormFile"/> request type.
    /// </summary>
    /// <param name="builder">The <see cref="RouteHandlerBuilder"/>.</param>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    /// <seealso cref="EndpointBuilder"/>
    /// <seealso cref="RouteHandlerBuilder"/>
    /// <seealso cref="EndpointMetadataCollection"/>
    public static RouteHandlerBuilder AcceptsFormFile(this RouteHandlerBuilder builder)
        => builder.Accepts<IFormFile>("multipart/form-data");

    /// <summary>
    /// Adds a <see cref="IAcceptsMetadata"/> to <see cref="EndpointBuilder.Metadata"/> for the <see cref="IFormFileCollection"/> request type.
    /// </summary>
    /// <param name="builder">The <see cref="RouteHandlerBuilder"/>.</param>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to further customize the endpoint.</returns>
    /// <seealso cref="EndpointBuilder"/>
    /// <seealso cref="RouteHandlerBuilder"/>
    /// <seealso cref="EndpointMetadataCollection"/>
    public static RouteHandlerBuilder AcceptsFormFileCollection(this RouteHandlerBuilder builder)
        => builder.Accepts<IFormFileCollection>("multipart/form-data");
}
