using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using HackerNewsAPI.Services;
using HackerNewsAPI.Services.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using HackerNewsAPI.Models;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.TypeInfoResolver = JsonContext.Default;
});
builder.Services.AddHttpClient<HackerNewsService>();
builder.Services.AddHttpClient<IHackerNewsService, HackerNewsService>();
builder.Services.Decorate<IHackerNewsService, CachedHackerNewsService>();
builder.Services.AddMemoryCache();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();