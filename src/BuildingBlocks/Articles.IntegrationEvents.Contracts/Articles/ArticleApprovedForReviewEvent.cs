using Articles.Abstractions.Events.Dtos;

namespace Articles.IntegrationEvents.Contracts;

public sealed record ArticleApprovedForReviewEvent(ArticleDto Article);
