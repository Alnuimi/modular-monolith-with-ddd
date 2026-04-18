using Blocks.AspNetCore.Filters;
using Blocks.AspNetCore.Middleware;
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


// todo migrate - create the first migration
//
if (app.Environment.IsDevelopment())
{
    // todo seed test data
}
#endregion


app.Run();

