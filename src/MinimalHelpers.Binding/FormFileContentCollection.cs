using Microsoft.AspNetCore.Http;

namespace MinimalHelpers.Binding;

public class FormFileContentCollection
{
    public IFormFileCollection Content { get; }

    public FormFileContentCollection(IFormFileCollection files)
    {
        Content = files;
    }

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