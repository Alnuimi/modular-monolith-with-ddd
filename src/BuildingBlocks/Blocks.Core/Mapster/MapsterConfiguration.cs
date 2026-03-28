using System.Reflection;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blocks.Core.Mapster;

public static class MapsterConfiguration
{
    public static IServiceCollection AddMapster(this IServiceCollection services, Assembly? assembly = null)
    {
        if (assembly is null)
        {
            assembly = Assembly.GetCallingAssembly()!;
        }

        TypeAdapterConfig.GlobalSettings.Scan(assembly);
        
        return services;
    }
}