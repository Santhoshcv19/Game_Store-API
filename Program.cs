using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen; 
using GameStore.Api.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

// Add CORS services and configure a policy.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "My API - V1",
            Version = "v1"
        }
     );

     var filePath = Path.Combine(System.AppContext.BaseDirectory, "GameStore.Api.xml");
     c.IncludeXmlComments(File.Exists(filePath) ? filePath : throw new Exception("XML file not found"));
     // or c.IncludeXmlComments(typeof(MyController).Assembly));
});

var app = builder.Build();

// Apply the CORS policy to the request pipeline.
app.UseCors("AllowAll");

app.UseSwagger();

// Enable middleware to serve Swagger UI
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameStore API V1");
    c.RoutePrefix = "swagger"; // Serve Swagger UI at /swagger
});


app.UseStaticFiles();

app.MapGet("/redoc", async context => 
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync(Path.Combine(app.Environment.WebRootPath, "redoc.html"));
});

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

app.Run();
