using System.ComponentModel.DataAnnotations;

namespace Blocks.AspNetCore.Grpc;

public sealed class GrpcServicesOptions
{
    public GrpcRetrySettings Retry { get; init; } = null!;
    public Dictionary<string, GrpcServiceSettings> Services { get; init; } = null!;
}

public sealed class GrpcServiceSettings
{
    public string Url { get; init; } = null!;
    
    public bool EnableRetry { get; init; }
}

public sealed class GrpcRetrySettings
{
    [Range(1, 10)]
    public int Count { get; init; }
    
    [Range(10, 10000)]
    public int InitialDelayMs { get; init; } // time in milliseconds
}