using Microsoft.AspNetCore.Http;

namespace MinimalHelpers.Binding;

/// <summary>
/// A class that holds a file sent with the <see cref="HttpRequest"/>.
/// </summary>
/// <seealso cref="IFormFile"/>
/// <seealso cref=" HttpRequest"/>
public class FormFileContent
{
    /// <summary>
    /// The reference to the file sent with the <see cref="HttpRequest"/>.
    /// </summary>
    /// <seealso cref="HttpRequest"/>
    public IFormFile Content { get; }

    internal FormFileContent(IFormFile file)
    {
        Content = file;
    }

    /// <summary>
    /// Reads the <see cref="HttpRequest"/> and extract the first file sent, if any.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> that represents the current request.</param>
    /// <returns>An object of type <see cref="FormFileContent"/>, if the request contains a valid file; otherwise, null.</returns>
    /// <seealso cref="FormFileContent"/>
    /// <seealso cref="HttpContext"/>
    /// <seealso cref="HttpRequest"/>
    public static async ValueTask<FormFileContent?> BindAsync(HttpContext context)
    {
        var request = context.Request;
        if (!request.HasFormContentType)
        {
            return null;
        }

        var form = await request.ReadFormAsync();
        var file = form.Files?.ElementAtOrDefault(0);

        if (file is null)
        {
            return null;
        }

        var result = new FormFileContent(file);
        return result;
    }
}
