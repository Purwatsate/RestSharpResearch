using ApiClient.Endpoints;
using ApiClient.Services;
using ApiClient.Services.Interface;
using ApiShared.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiClientSettings>(
    builder.Configuration.GetSection("ApiClientSettings"));

builder.Services.AddSingleton<ITokenProvider>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<ApiClientSettings>>();
    return new BearerTokenProvider(settings);
});

builder.Services.AddSingleton<IMyApiClient>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<ApiClientSettings>>();
    var tokenProvider = provider.GetRequiredService<ITokenProvider>();
    return new MyApiClient(settings, tokenProvider);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapWeatherEndpoints();

app.Run();
