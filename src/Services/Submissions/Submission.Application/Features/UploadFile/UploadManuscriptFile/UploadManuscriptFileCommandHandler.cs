using Blocks.EntityFramework;
using Submission.Persistence;

namespace Submission.Application.Features.UploadFile;

internal sealed class UploadManuscriptFileCommandHandler(
    ArticleRepository _articleRepository,
    AssetTypeDefinitionRepository _assetTypeRepository)
    : IRequestHandler<UploadManuscriptFileCommand, IdResponse>
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

        await _articleRepository.SaveChangesAsync(cancellationToken);
        return new IdResponse(asset.Id);
    }
}