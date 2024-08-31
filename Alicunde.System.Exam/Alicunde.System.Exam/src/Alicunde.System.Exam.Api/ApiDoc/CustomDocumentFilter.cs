using Alicunde.System.Exam.Contracts;
using Alicunde.System.Exam.Domain;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Alicunde.System.Exam.Api.ApiDoc;

public class CustomDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        context.SchemaGenerator.GenerateSchema(typeof(Bank), context.SchemaRepository);
        var dictionaryPath = swaggerDoc.Paths.ToDictionary(x => x.Key.ToLowerInvariant(), x => x.Value);
        var newPaths = new OpenApiPaths();
        foreach(var path in dictionaryPath)
        {
            newPaths.Add(path.Key, path.Value);
        }
        swaggerDoc.Paths = newPaths;
    }
}
