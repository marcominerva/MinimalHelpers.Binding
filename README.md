# Minimal APIs Binding Helpers

> This repository is not maintained anymore. Support for IForm and IFormFile binding in Minimal APIs has been added in NET 7.0.

[![GitHub Super-Linter](https://github.com/marcominerva/MinimalHelpers.Binding/workflows/Lint%20Code%20Base/badge.svg)](https://github.com/marketplace/actions/super-linter)
[![Nuget](https://img.shields.io/nuget/v/MinimalHelpers.Binding)](https://www.nuget.org/packages/MinimalHelpers.Binding)
[![Nuget](https://img.shields.io/nuget/dt/MinimalHelpers.Binding)](https://www.nuget.org/packages/MinimalHelpers.Binding)

A library that provides Binding helpers for Minimal API projects.

**Installation**

The library is available on [NuGet](https://www.nuget.org/packages/MinimalHelpers.Binding). Just search *MinimalHelpers.Binding* in the **Package Manager GUI** or run the following command in the **Package Manager Console**:

    Install-Package MinimalHelpers.Binding

**Usage**

***IFormFile and IFormFileCollection binding***

Use a `FormFileContent` or `FormFileContentCollection` argument in the route handler that must receive a single file or a collection of files, then call the corresponding `Accepts*` extension method on the endpoint definition:

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

Add the `FormFile` Operation Filter to Swagger, so that it will be able to correctly handle file input:

    builder.Services.AddSwaggerGen(options =>
    {
        options.AddFormFile();
    });

**Contribute**

The project is constantly evolving. Contributions are welcome. Feel free to file issues and pull requests on the repo and we'll address them as we can. 
