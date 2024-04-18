using FeatureVotingSystem.Shared.Entities.Exceptions;

namespace FeatureVotingSystem.Core.Products.Features.CreateProduct;

public class CreateProductService : ICreateProductService
{
    private readonly ICreateProductRepository _createProductRepository;

    public CreateProductService(ICreateProductRepository createProductRepository)
    {
        _createProductRepository = createProductRepository;
    }

    public async Task CreateProductAsync(CreateProductRequest product, int userId)
    {
        if (product == null)
            throw new ProductBadRequestException("Product sent from a client is null");

        product.SetUserId(userId);

        var productWithSameName =
            await _createProductRepository.CheckIfProductWithGivenNameAlreadyExistsAsync(product.Name);

        if (productWithSameName > 0)
            throw new ProductBadRequestException($"Product with name: {product.Name} already exists");

        var affectedRows = await _createProductRepository.CreateProductAsync(product);

        if (affectedRows == 0)
            throw new ProductBadRequestException("Product was not created due to some exception");
    }
}