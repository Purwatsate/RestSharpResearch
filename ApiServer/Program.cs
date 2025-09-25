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

app.MapGet("/weather", () =>
{
    return Results.Ok(new { Temp = 27, City = "Yangon" });
});

app.MapGet("/fail", () =>
{
    // Simulate a server-side error
    return Results.Problem("Something went wrong", statusCode: 500);
});

app.Run();
