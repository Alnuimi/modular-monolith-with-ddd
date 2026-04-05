using Articles.Abstractions.Events.Dtos;

namespace Articles.Abstractions.Events;

public sealed record ArticleApprovedForReviewEvent(ArticleDto Article);