using Articles.Abstractions.Enums;
using Blocks.Domain.Entities;

namespace ArticleHub.Domain.Articles;

public class Article : AggregateRoot
{
    public required string Title { get; init; }
    public string? Dio { get; init; }
    public ArticleStage Stage { get; private set; }
    public required virtual int SubmittedById { get; set; }
    public virtual Person SubmittedBy { get; init; } = null!;

    public DateTime SubmittedOn { get; init; }
    public DateTime? AcceptedOn { get; init; }
    public DateTime? PublishedOn { get; init; }

    public required int JournalId { get; init; }
    public Journal Journal { get; set; } = null!;

    // private readonly List<ArticleActor> _actors = new();
    public List<ArticleActor> Actors {get; set; } = new List<ArticleActor>(); // todo- we change to read only and create from behavior _actors.AsReadOnly();

}
