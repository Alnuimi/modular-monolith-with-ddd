using System;

namespace Blocks.Core.GraphQL;

public class QueryResult<T>
{
    public IEnumerable<T> Items { get; set; } = null!;
    public int Count { get; set; }
    public int PageNumber { get; set; }
}
