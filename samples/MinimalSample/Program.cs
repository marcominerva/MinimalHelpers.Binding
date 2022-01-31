using MinimalHelpers.Binding;
using FormFile = MinimalHelpers.Binding.FormFile;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<OpenApiUploadOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/file", (FormFile file) =>
{
    return Results.NoContent();
})
.Accepts<FormFile>("multipart/form-data");

app.Run();