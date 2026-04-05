using Blocks.Domain;
using Submission.Domain.Entities;

namespace Submission.Domain.Events;

public record ArticleApproved(Article Article) : IDomainEvent;