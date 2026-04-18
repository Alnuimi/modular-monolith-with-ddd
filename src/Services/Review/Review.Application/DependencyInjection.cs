using System.Reflection;
using Blocks.Core.Mapster;
using Blocks.MediatR.Behaviors;
using Blocks.Messaging.MassTransit;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Review.Application.Features.Invitations.InviteReviewer;

namespace Review.Application;

public static  class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // talk - fluid vs normal
        services
        .AddMapsterConfigsFromCurrentAssembly()                                         // Scanning for mapping registrations in the current assembly                   
                                                                                       // .AddValidatorsFromCurrentAssemblyContaining()                                                   // Scanning for validators in the current assembly
        .AddValidatorsFromAssemblyContaining<InviteReviewerCommandValidator>() // Register Fluent valiators as transient 
        .AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>)); 
            config.AddOpenBehavior(typeof(AssignUserIdBehavior<,>)); 
        })
        .AddMassTransitWithRabbitMq(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}