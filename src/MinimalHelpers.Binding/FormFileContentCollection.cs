using Microsoft.AspNetCore.Http;

namespace MinimalHelpers.Binding;

/// <summary>
/// A class that holds a collection of files sent with the <see cref="HttpRequest"/>.
/// </summary>
/// <seealso cref="IFormFileCollection"/>
/// <seealso cref=" HttpRequest"/>
public class FormFileContentCollection
{
    /// <summary>
    /// The reference to the collection of files sent with the <see cref="HttpRequest"/>.
    /// </summary>
    /// <seealso cref="HttpRequest"/>
    public IFormFileCollection Content { get; }

    internal FormFileContentCollection(IFormFileCollection files)
    {
        Content = files;
    }

    /// <summary>
    /// Reads the <see cref="HttpRequest"/> and extract all the files it contains.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> that represents the current request.</param>
    /// <returns>An object of type <see cref="FormFileContentCollection"/> containing the collection of files.</returns>
    /// <seealso cref="FormFileContentCollection"/>
    /// <seealso cref="HttpContext"/>
    /// <seealso cref="HttpRequest"/>
    public static async ValueTask<FormFileContentCollection?> BindAsync(HttpContext context)
    {
        var request = context.Request;
        if (!request.HasFormContentType)
        {
            return null;
        }

        var form = await request.ReadFormAsync();
        var files = form.Files;

        if (files is null)
        {
            return null;
        }

        var result = new FormFileContentCollection(files);
        return result;
    }
}