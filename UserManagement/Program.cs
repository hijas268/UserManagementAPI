using Microsoft.OpenApi.Models;
using UserManagement.Utils.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Application-specific services
builder.Services.AddApplicationConfigurations(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

// Configure Swagger / OpenAPI
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "My API",
//        Version = "v1",
//        Description = "User Management API"
//    });


//});

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    // Serve Swagger JSON
    app.UseSwagger(c =>
    {
        c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
        {
     
            swaggerDoc.Info.Version = "v1"; 
        });
    });

    // Serve Swagger UI at root
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.RoutePrefix = string.Empty; // Swagger UI available at https://localhost:7265/
    });

    app.UseDeveloperExceptionPage();
}
app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
