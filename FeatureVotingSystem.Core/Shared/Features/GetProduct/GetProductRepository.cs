using Dapper;
using FeatureVotingSystem.Core.Shared.Models;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.Shared.Features.GetProduct;

public class GetProductRepository : IGetProductRepository
{
    private readonly DapperContext _context;

    public GetProductRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetProductAsync(int productId)
    {
        var query = "SELECT * FROM Products WHERE Id = @Id";

        IEnumerable<Product>? products = null;

        await using var connection = await _context.CreateConnectionAsync();
        {
            products = await connection.QueryAsync<Product>(query, new Product() { Id = productId });
        }

        return products.SingleOrDefault();
    }
}