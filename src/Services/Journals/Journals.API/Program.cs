using Blocks.AspNetCore.Middleware;
using FastEndpoints;
using FastEndpoints.Swagger;
using Journals.API;
using Journals.Persistence;
using Blocks.FastEndpoints;
using Journals.API.Features.Journals;

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
    .UseMiddleware<GlobalExceptionMiddleware>()
    .UseSwagger()
    .UseSwaggerUI()
    .UseRedis()
    .UseHttpsRedirection()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    // .UseFastEndpoints(config =>
    // {
    //     config.Endpoints.Configurator = ed =>
    //     {
    //         ed.PreProcessor<AssignUserIdPreProcessor>(Order.Before);
    //     };
    // })
    .UseCustomFastEndpoints()
    .UseSwaggerGen();
   

#endregion
app.MapGrpcService<JournalGrpcService>();
app.Run();