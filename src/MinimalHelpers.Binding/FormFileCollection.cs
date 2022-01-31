using Microsoft.AspNetCore.Http;

namespace MinimalHelpers.Binding;

public class FormFileCollection : Microsoft.AspNetCore.Http.FormFileCollection
{
    public static async ValueTask<FormFileCollection?> BindAsync(HttpContext context)
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

        var result = new FormFileCollection();
        foreach (var file in files)
        {
            result.Add(file);
        }

        return result;
    }
}