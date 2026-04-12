using System;
using System.Text.Json.Serialization;

namespace ArticleHub.API.Articles.SearchArticles;

public class SearchArticlesQuery
{
    public required object Filter { get; init; }
    public Pagination Pagination { get; init; } = new();
}

public sealed class Pagination
{
    private const int MaxLimit = 100;

    [JsonConstructor]
    public Pagination(int limit = 20, int offset = 0)
    {
        Limit = limit > MaxLimit ? MaxLimit : limit;
        Offset = offset < 0 ? 0 : offset;
    }
    public int Limit { get; private set;}
    public int Offset { get; private set;}
}