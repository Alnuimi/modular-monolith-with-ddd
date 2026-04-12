using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Blocks.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConcreteImplementationOfGeneric(
        this IServiceCollection services,
        Type genericBaseType,
        Assembly[]? assemblies = null,
        ServiceLifetime lifetime = ServiceLifetime.Scoped
    )
    {
        assemblies ??= new[] {Assembly.GetCallingAssembly()};

        var implementations = assemblies
        .SelectMany(a => a.GetTypes())
        .Where(t => 
            t.IsClass &&
            !t.IsAbstract &&
            InheritsFromGenericType(t, genericBaseType)
        );

        foreach(var implementation in implementations)
        {
            
            services.Add(new ServiceDescriptor(implementation, implementation, lifetime));
        }

        return services;

    }

    private static bool InheritsFromGenericType(Type type, Type genericBaseType)
    {
       while(type != null && type != typeof(object))
        {
            var currentType = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
            if (currentType == genericBaseType)
                return true;

            type = type.BaseType;
        }
        return false;
    }
}
