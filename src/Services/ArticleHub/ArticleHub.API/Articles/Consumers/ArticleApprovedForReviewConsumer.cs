using ArticleHub.Domain.Articles;
using ArticleHub.Persistence;
using Articles.IntegrationEvents.Contracts;
using Articles.IntegrationEvents.Contracts.Dtos;
using Blocks.Core.Mapster;
using Blocks.Exceptions;
using Mapster;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ArticleHub.API.Articles.Consumers;

public class ArticleApprovedForReviewConsumer(ArticleHubDbContext _dbContext) : IConsumer<ArticleApprovedForReviewEvent>
{
    public async Task Consume(ConsumeContext<ArticleApprovedForReviewEvent> context)
    {
        var articleDto = context.Message.Article;

        if (await _dbContext.Articles.AnyAsync(a => a.Id == articleDto.Id))
        {
            throw new BadRequestException("Article was alreay approved for review.");
        }

        var journal = await GetOrCreateJournaAsync(articleDto, context.CancellationToken);

        var article = articleDto.AdaptWith<Article>(article =>
        {
            article.Journal = journal;
            article.SubmittedById = articleDto.SubmittedBy.Id;
        });

        await CreateActorAsync(articleDto, article, context.CancellationToken);

        await _dbContext.Articles.AddAsync(article, context.CancellationToken);

        await _dbContext.SaveChangesAsync(context.CancellationToken);
    }

    private async Task<Journal> GetOrCreateJournaAsync(ArticleDto articleDto, CancellationToken ct = default)
    {
        var journal = await _dbContext.Journals.SingleOrDefaultAsync(j => j.Id == articleDto.Journal.Id, ct);
        if (journal is null)
        {
            journal = articleDto.Journal.Adapt<Journal>();
            await _dbContext.Journals.AddAsync(journal, ct);
        }

        return journal;
    }

    private async Task CreateActorAsync(ArticleDto articleDto , Article article, CancellationToken ct = default)
    {
        foreach (var actorDto in articleDto.Actors)
        {
            var person = await _dbContext.Persons.SingleOrDefaultAsync(p => p.Id == actorDto.Person.Id, ct);
            if (person is null)
            {
                person = actorDto.Person.Adapt<Person>();
                await _dbContext.Persons.AddAsync(person, ct);
            }

            article.Actors.Add(new ArticleActor
            {
                ArticleId = article.Id,
                PersonId = person.Id,
                Role = actorDto.Role
            });
        }
    }
}
