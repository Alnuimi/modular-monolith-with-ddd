using Auth.API;
using Auth.API.Features.Persons;
using Auth.Application;
using Auth.Persistence;
using Blocks.AspNetCore.Middleware;
using Blocks.EntityFramework;
using Blocks.FastEndpoints;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureApiOptions(builder.Configuration);

#region Add Services

builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration);

#endregion

var app = builder.Build();
#region InitData
//insight - explain when is the best time to run the migration, integrate the migration in the CI pipeline
app.Migrate<AuthDbContext>();
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
    .UseHttpsRedirection()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseCustomFastEndpoints()
    .UseSwaggerGen();


app.MapGrpcService<PersonGrpcService>();
#endregion

app.Run();


