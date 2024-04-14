using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.Products.Features.DeleteProduct;

public class DeleteProductRepository : IDeleteProductRepository
{
    private readonly DapperContext _context;

    public DeleteProductRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SoftDeleteProductAsync(int productId)
    {
        var command = "dbo.spDeleteProduct @ProductId";
        
        await using var connection = await _context.CreateConnectionAsync();
        
        var affectedRows = await connection.ExecuteAsync(command, new { ProductId = productId });

        return affectedRows;
    }
}