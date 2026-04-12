using Articles.IntegrationEvents.Contracts;
using Articles.IntegrationEvents.Contracts.Dtos;
using Blocks.Domain;
using Blocks.Exceptions;
using FileStorage.Contracts;
using Mapster;
using MassTransit;
using Review.Application.Features.FileStorage;
using Review.Domain.Assets;
using Review.Domain.Shared;
using Review.Persistence.Repositories;

namespace Review.Application.Features.Articles.InitializeFromSubmission;

public sealed class ArticleApprovedForReviewEventHandler(
    ReviewDbContext _dbContext,
    ArticleRepository _articleRepository,
    Repository<Journal> _journalRepository,
    Repository<Person> _personRepository,
    AssetTypeDefinitionRepository _assetTypeDefinitionRepository,
    IFileService _fileService,
    FileServiceFactory _fileServiceFactory)
     : IConsumer<ArticleApprovedForReviewEvent>
{
    public async Task Consume(ConsumeContext<ArticleApprovedForReviewEvent> context)
    {
        var articleDto = context.Message.Article;
        if (_articleRepository.Entity.Any(e => e.Id == articleDto.Id))
        {
            throw new BadRequestException($"Article({articleDto.Id}) already exists");
        }

        var journal = await GetOrCreateJournalAsync(articleDto);
        var actors = await CreateActorsAsync(articleDto);
        var assets = await CreateAssets(articleDto.Assets, articleDto.Id);

        var article = Article.AcceptSubmitted(articleDto, actors, assets);
         journal.AddArticle(article);
         
         await _dbContext.SaveChangesAsync(context.CancellationToken);
    }

    private async  Task<IEnumerable<Asset>> CreateAssets(List<AssetDto> assetDtos, int articleId, CancellationToken ct = default)
    {
        var assets = new List<Asset>();
        foreach (var assetDto in assetDtos)
        {
            var assetTypeDefinition = _assetTypeDefinitionRepository.GetById(assetDto.Type);
            var asset = Asset.CreateFromSubmission(assetDto, assetTypeDefinition, articleId);
            
            // todo - download the files from submission upload to review

            var submissionFileService = _fileServiceFactory(FileStorageType.Submission);
            var (fileStream, fileMetadata) = await submissionFileService.DownloadFileAsync(asset.File.FileServerId, ct);

            var fileUploadRequest = new FileUploadRequest(fileMetadata.StoragePath, fileMetadata.FileName, fileMetadata.ContentType, fileMetadata.FileSize);
            fileMetadata = await _fileService.UploadFileAsync(fileUploadRequest, fileStream, ct: ct);
            
            asset.CreateFile(fileMetadata, assetTypeDefinition);
            
            assets.Add(asset);
        }

        return assets;
    }

    private async Task<IEnumerable<ArticleActor>> CreateActorsAsync(ArticleDto articleDto)
    {
        var actors = new List<ArticleActor>();
        foreach (var actorDto in articleDto.Actors)
        {
            var person = await _personRepository.GetByIdAsync(actorDto.Person.Id);

            ArticleActor actor = default!;
            
            if (actorDto.Role == UserRoleType.AUT || actorDto.Role == UserRoleType.CORAUT)
            {
                if (person is null)
                {
                    person = actorDto.Person.Adapt<Author>();
                }

                actor = new ArticleAuthor(actorDto.ContributionAreas)
                {
                    PersonId = person.Id,
                    Person = person,
                    Role = actorDto.Role
                };
            }

            else if (actorDto.Role == UserRoleType.REVED)
            {
                if (person is null)
                {
                    person = actorDto.Person.Adapt<Editor>();
                }

                actor = new ArticleActor()
                {
                    PersonId = person.Id,
                    Person = person,
                    Role = actorDto.Role
                };
            }
            else
            {
                throw new DomainException($"Unknown role for {actorDto.Person.Email}");
            }
            actors.Add(actor);
        }

        return actors;
    }

    private async Task<Journal> GetOrCreateJournalAsync(ArticleDto articleDto)
    {
        var journal = await _journalRepository.FindByIdAsync(articleDto.Journal.Id);
        if (journal is null)
        {
            journal = articleDto.Journal.Adapt<Journal>();
            await _journalRepository.AddAsync(journal);
        }

        return journal;
    }
}