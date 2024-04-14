using FeatureVotingSystem.Core.Shared.Features.GetProduct;
using FeatureVotingSystem.Core.Shared.Models;
using FeatureVotingSystem.Shared.Entities.Exceptions;

namespace FeatureVotingSystem.Core.Products.Features.UpdateProduct;

public class UpdateProductService : IUpdateProductService
{
    private readonly IUpdateProductRepository _updateProductRepository;
    private readonly IGetProductRepository _getProductRepository;

    public UpdateProductService(IUpdateProductRepository updateProductRepository, IGetProductRepository getProductRepository)
    {
        _updateProductRepository = updateProductRepository;
        _getProductRepository = getProductRepository;
    }

    public async Task UpdateProductAsync(UpdateProductRequest product, int userId)
    {
        if (product == null)
            throw new ProductBadRequestException("Product sent from a client is null");

        Product? productFromDb = await _getProductRepository.GetProductAsync(product.Id);

        if (productFromDb is null)
            throw new ProductNotFoundException(product.Id);

        if (productFromDb.UserId != userId)
            throw new UnauthorizedAccessException($"Please, update only your products");

        var validationResult = ProductValidations.ValidateUpdateProductRequest(product);

        if (!validationResult.IsValid)
            throw new ProductBadRequestException(validationResult.Errors.First().ErrorMessage);

        int affectedRows = await _updateProductRepository.UpdateProductAsync(product);

        if (affectedRows == 0)
            throw new ProductBadRequestException("Product was not updated due to some exception");
    }

}
