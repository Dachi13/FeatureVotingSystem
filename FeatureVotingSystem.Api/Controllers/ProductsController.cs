using FeatureVotingSystem.Core.Products.Features.CreateProduct;
using FeatureVotingSystem.Core.Products.Features.DeleteProduct;
using FeatureVotingSystem.Core.Products.Features.UpdateProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FeatureVotingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("MyApiUserPolicy", AuthenticationSchemes = "Bearer")]
    public class ProductsController : ControllerBase
    {
        private readonly ICreateProductService _createProductsService;
        private readonly IUpdateProductService _updateProductsService;
        private readonly IDeleteProductService _deleteProductsService;

        public ProductsController(
            ICreateProductService createProductService,
            IUpdateProductService updateProductService,
            IDeleteProductService deleteProductService
        )
        {
            _createProductsService = createProductService;
            _updateProductsService = updateProductService;
            _deleteProductsService = deleteProductService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest product)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            await _createProductsService.CreateProductAsync(product, userId);

            return Ok("Product has been created");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductRequest product)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            await _updateProductsService.UpdateProductAsync(product, userId);

            return Ok("Product has been updated");
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            await _deleteProductsService.SoftDeleteProductAsync(id, userId);

            return Ok("Product has been deleted");
        }
    }
}