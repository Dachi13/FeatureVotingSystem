using FeatureVotingSystem.Core.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Shared.Models;
using FeatureVotingSystem.Shared.Entities.Exceptions;

namespace FeatureVotingSystem.Core.Products.Features.DeleteProduct;

public class DeleteProductService : IDeleteProductService
{
    private readonly IDeleteProductRepository _deleteProductRepository;
    private readonly IGetProductRepository _getProductRepository;

    public DeleteProductService(IDeleteProductRepository deleteProductRepository, IGetProductRepository getProductRepository)
    {
        _deleteProductRepository = deleteProductRepository;
        _getProductRepository = getProductRepository;
    }

    public async Task SoftDeleteProductAsync(int productId, int userId)
    {
        if (productId <= 0)
            throw new IdParameterBadRequestException(productId);

        Product? productFromDb = await _getProductRepository.GetProductAsync(productId);

        if (productFromDb is null || productFromDb.IsDeleted == 1)
            throw new ProductNotFoundException(productId);

        if (productFromDb.UserId != userId)
            throw new UnauthorizedAccessException($"Please, delete only your products");

        int affectedRows = await _deleteProductRepository.SoftDeleteProductAsync(productId);

        if (affectedRows == 0)
            throw new ProductBadRequestException("Product was not deleted due to some exception");
    }

}
