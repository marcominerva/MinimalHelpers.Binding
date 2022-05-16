using MinimalHelpers.Binding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add the Operation Filter that allows to correctly handle file input in Swagger.    
    options.AddFormFile();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/single-file", (FormFileContent file) =>
{
    return Results.Ok(new
    {
        file.Content.FileName,
        file.Content.ContentType,
        file.Content.Length
    });
})
.AcceptsFormFile();

app.MapPost("/api/multiple-files", (FormFileContentCollection files) =>
{
    return Results.Ok(files.Content.Select(file => new
    {
        file.FileName,
        file.ContentType,
        file.Length
    }));
})
.AcceptsFormFileCollection();

app.Run();