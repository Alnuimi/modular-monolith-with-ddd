using Articles.Abstractions.Enums;
using Blocks.Domain.Entities;
using Review.Domain.Shared;

namespace Review.Domain.Articles;

public class Article : AggregateRoot
{
    public required  string Title { get; init; }
    public ArticleType Type { get; init; }
    public string Scope { get; init; } = default!;
    public DateTime? SubmittedOn {get; init; }
    public int? SubmittedById { get; init; }
    public Person? SubmittedBy {get; init;}
    public ArticleStage Stage { get; private set; }
}