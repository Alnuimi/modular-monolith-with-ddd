using ArticleHub.API;
using ArticleHub.Persistence;
using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
    .UseAuthentication()
    .UseAuthorization();
    // .UseMiddleware<GlobalExceptionMiddleware>() // todo-building 
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
