using System.Reflection;
using System.Runtime.CompilerServices;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blocks.Core.Mapster;

public static class MapsterConfiguration
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static IServiceCollection AddMapsterConfigsFromCurrentAssembly(this IServiceCollection services, Assembly? assembly = null)
    {
        if (assembly is null)
        {
            assembly = Assembly.GetCallingAssembly()!;
        }

        TypeAdapterConfig.GlobalSettings.Scan(assembly);
        
        return services;
    }
}