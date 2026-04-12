using Articles.IntegrationEvents.Contracts.Dtos;

namespace Articles.IntegrationEvents.Contracts;

public sealed record ArticlePublishedEvent(ArticleDto Article);