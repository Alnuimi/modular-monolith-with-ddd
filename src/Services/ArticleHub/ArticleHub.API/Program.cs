using ArticleHub.API;
using ArticleHub.Persistence;
using Blocks.AspNetCore.Middleware;
using Blocks.EntityFramework;
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
#region InitData
//insight - explain when is the best time to run the migration, integrate the migration in the CI pipeline
app.Migrate<ArticleHubDbContext>();
if (app.Environment.IsDevelopment())
{
    // app.Services.SeedTestData();
}
#endregion
#region Use
app
    .UseMiddleware<GlobalExceptionMiddleware>()
    .UseSwagger()
    .UseSwaggerUI()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();
    // .UseMiddleware<RequestContextMiddleware>()
    // .UseMiddleware<ResponseTimingMiddleware>();

var apiGroup = app.MapGroup("/api");
apiGroup.MapCarter();

#endregion


app.Run();
