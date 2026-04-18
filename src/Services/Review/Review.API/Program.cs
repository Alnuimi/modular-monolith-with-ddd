using Blocks.AspNetCore.Filters;
using Blocks.AspNetCore.Middleware;
using Blocks.EntityFramework;
using Carter;
using Review.API;
using Review.Application;
using Review.Persistence;

var builder = WebApplication.CreateBuilder(args);

#region Add Services

builder.Services
    .ConfigureApiOptions(builder.Configuration);

builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration);

#endregion

var app = builder.Build();
#region InitData
//insight - explain when is the best time to run the migration, integrate the migration in the CI pipeline
app.Migrate<ReviewDbContext>();
if (app.Environment.IsDevelopment())
{
    // app.Services.SeedTestData();
}
#endregion
#region Use Services

app
    .UseMiddleware<GlobalExceptionMiddleware>()
    .UseSwagger()
    .UseSwaggerUI()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();

var apiGroup = app.MapGroup("/api"); //.AddEndpointFilter<AssignUserIdFilter>();
apiGroup.MapCarter();


#endregion


app.Run();

