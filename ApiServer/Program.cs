using ApiShared.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    Console.WriteLine($"Incoming request: {context.Request.Method} {context.Request.Path}");
    Console.WriteLine($"Host: {context.Request.Host}");

    foreach (var header in context.Request.Headers)
    {
        Console.WriteLine($"Header: {header.Key}: {header.Value}");
    }

    // Query-string parameters (from AddQueryParameter)
    if (context.Request.Query.Count != 0)
    {
        foreach (var kvp in context.Request.Query)
        {
            Console.WriteLine($"QueryParam: {kvp.Key} = {kvp.Value}");
        }
    }

    // Route values (from AddUrlSegment -> matched by routing)
    if (context.Request.RouteValues.Count != 0)
    {
        foreach (var kvp in context.Request.RouteValues)
        {
            Console.WriteLine($"RouteValue: {kvp.Key} = {kvp.Value}");
        }
    }
    await next();
});

app.MapGet("/weather/{r1}/{r2}", (string r1, string r2) =>
{
    Console.WriteLine($"Route Value 1 : {r1}");
    Console.WriteLine($"Route Value 2 : {r2}");
    return Results.Ok(new WeatherDto("Yangon", 24));
});

app.MapGet("/fail", () =>
{
    // Simulate a server-side error
    return Results.Problem("Something went wrong", statusCode: 500);
});

app.Run();
