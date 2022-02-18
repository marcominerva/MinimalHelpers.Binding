using Microsoft.AspNetCore.Http;

namespace MinimalHelpers.Binding;

public class FormFileContent
{
    public IFormFile Content { get; }

    public FormFileContent(IFormFile file)
    {
        Content = file;
    }

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
