using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.Products.Features.UpdateProduct;

public class UpdateProductRepository : IUpdateProductRepository
{
    private readonly DapperContext _context;

    public UpdateProductRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> UpdateProductAsync(UpdateProductRequest product)
    {
        var command = "UPDATE Products SET Name = @Name, ShortDesc = @ShortDesc where Id = @Id";

        await using var connection = await _context.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(command, product);

        return affectedRows;
    }
}