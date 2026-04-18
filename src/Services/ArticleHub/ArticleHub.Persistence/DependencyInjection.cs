using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Blocks.Core.Extensions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleHub.Persistence;

public static  class DependencyInjection
{
    public static IServiceCollection AddPersistencesServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ArticleHubDbContext>(options =>
        {
           options.UseNpgsql(configuration.GetConnectionString("Database")); 
        });

        var hasuraOption = configuration.GetSectionByTypeName<HasuraOptions>();

        services.AddSingleton(_ =>
        {
            var graphQLClientOptions = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(new Uri(hasuraOption.Endpoint), "v1/graphql")
            };

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var graphQLHttpClient = new GraphQLHttpClient(graphQLClientOptions, new SystemTextJsonSerializer(jsonSerializerOptions));

            graphQLHttpClient.HttpClient.DefaultRequestHeaders.Add("x-hasura-admin-secret", hasuraOption.AdminSecret);
            
            return graphQLHttpClient;
        });

        services.AddScoped<ArticleGraphQLReadStore>();


        return services;
    }
}
