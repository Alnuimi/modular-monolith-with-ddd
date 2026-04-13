using Blocks.AspNetCore.Middleware;
using FastEndpoints;
using FastEndpoints.Swagger;
using Journals.API;
using Journals.Persistence;
using Blocks.FastEndpoints;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .ConfigureApiOptions(builder.Configuration);

#region Add Services

builder.Services
    .AddApiServices(builder.Configuration)
     .AddPersistenceServices(builder.Configuration);

#endregion

var app = builder.Build();

#region Use
app
    .UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection()
    .UseRouting()
    .UseMiddleware<GlobalExceptionMiddleware>()
    .UseFastEndpoints(config =>
    {
        config.Endpoints.Configurator = ed =>
        {
            ed.PreProcessor<AssignUserIdPreProcessor>(Order.Before);
        };
    })
    .UseSwaggerGen()
    .UseAuthentication()
    .UseAuthorization();
   

#endregion

app.Run();