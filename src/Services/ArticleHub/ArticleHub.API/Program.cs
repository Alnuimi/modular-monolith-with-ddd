using ArticleHub.API;
using ArticleHub.Persistence;
using Blocks.AspNetCore.Middleware;
using Carter;

var builder = WebApplication.CreateBuilder(args);

#region Add
builder.Services
    .ConfigureApiOptions(builder.Configuration);  // Configure options

builder.Services
    .AddApiAndApplicationServices(builder.Configuration) // Register API/Infra/Application-specific services
    .AddPersistencesServices(builder.Configuration);     // Register Persistence-specific services
#endregion

var app = builder.Build();

#region Use
app
    .UseSwagger()
    .UseSwaggerUI()
    .UseRouting()
    .UseMiddleware<GlobalExceptionMiddleware>()
    .UseAuthentication()
    .UseAuthorization();
    // .UseMiddleware<RequestContextMiddleware>()
    // .UseMiddleware<ResponseTimingMiddleware>();

var api = app.MapGroup("/api");
api.MapCarter();

#endregion

#region InitData
// app.Migrate<ArticleHubDbContext>(); // todo-building

if (app.Environment.IsDevelopment())
{
    // app.SeedTestData(); // todo-building
}
#endregion

app.Run();
