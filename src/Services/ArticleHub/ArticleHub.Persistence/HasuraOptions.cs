using System;

namespace ArticleHub.Persistence;

public sealed class HasuraOptions
{
    public string Endpoint { get; init; } = null!;
    public string AdminSecret { get; init; } = null!;
    
}
