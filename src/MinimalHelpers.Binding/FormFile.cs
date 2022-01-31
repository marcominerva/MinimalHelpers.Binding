using Microsoft.AspNetCore.Http;

namespace MinimalHelpers.Binding;

public class FormFile
{
    public IFormFile Content { get; }

    public FormFile(IFormFile file)
    {
        Content = file;
    }

    public static async ValueTask<FormFile?> BindAsync(HttpContext context)
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

        var result = new FormFile(file);
        return result;
    }
}

