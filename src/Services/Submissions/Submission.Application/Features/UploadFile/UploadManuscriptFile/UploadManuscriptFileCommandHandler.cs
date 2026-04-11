using Blocks.EntityFramework;
using Blocks.MediatR.Messaging;
using FileStorage.Contracts;
using Submission.Persistence;

namespace Submission.Application.Features.UploadFile;

internal sealed class UploadManuscriptFileCommandHandler(
    ArticleRepository _articleRepository,
    AssetTypeDefinitionRepository _assetTypeRepository,
    IFileService _fileService)
    : ICommandHandler<UploadManuscriptFileCommand, IdResponse>
{
    public async Task<IdResponse> Handle(UploadManuscriptFileCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

        var assetType =  _assetTypeRepository.GetById(command.AssetType);

        Asset asset = null;
        if(!assetType.AllowsMultipleAssets)
         asset = article.Assets.FirstOrDefault(e => e.Type == assetType.Id);
        
        if(asset is null)
            asset = article.CreateAsset(assetType);
        
        // todo - upload the file
        var storagePath = asset.GenerateStorageFilePath(command.File.FileName);
        var fileMetadata = await _fileService.UploadFileAsync(storagePath, command.File, overwrite: true,
            tags: new Dictionary<string, string>
            {
                { "entity", nameof(Asset) },
                { "entityId", asset.Id.ToString() }
            }
            );

        try
        {
            asset.CreateFile(fileMetadata, assetType);

            await _articleRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _fileService.TryDeleteFileAsync(fileMetadata.FileId);
            throw;
        }

        return new IdResponse(asset.Id);
    }
}