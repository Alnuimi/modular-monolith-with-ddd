using System;
using Articles.Abstractions;
using Blocks.Domain;

namespace Review.Domain.Reviewers.Events;

public record ReviewerCreated(Reviewer Reviewer, IArticleAction Action) : IDomainEvent;
