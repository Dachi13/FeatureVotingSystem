using FeatureVotingSystem.Core.Products.Features.CreateProduct;
using FeatureVotingSystem.Core.Products.Features.UpdateProduct;
using FluentValidation.Results;

namespace FeatureVotingSystem.Core.Products.Features
{
    public static class ProductValidations
    {
        public static ValidationResult ValidateCreateProductRequest(CreateProductRequest product)
        {
            var validator = new CreateProductRequestValidator();
            var result = validator.Validate(product);

            return result;
        }

        public static ValidationResult ValidateUpdateProductRequest(UpdateProductRequest product)
        {
            var validator = new UpdateProductRequestValidator();
            var result = validator.Validate(product);

            return result;
        }
    }
}
