using Dapper;
using FeatureVotingSystem.Shared;

namespace FeatureVotingSystem.Core.Products.Features.CreateProduct;

public class CreateProductRepository : ICreateProductRepository
{
    private readonly DapperContext _context;

    public CreateProductRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> CheckIfProductWithGivenNameAlreadyExistsAsync(string name)
    {
        await using var connection = await _context.CreateConnectionAsync();

        return await connection.ExecuteScalarAsync<int>("dbo.spCheckIfProductWithGivenNameAlreadyExists @Name",
            new { Name = name });
    }

    public async Task<int> CreateProductAsync(CreateProductRequest product)
    {
        var command =
            "INSERT INTO Products (Name, ShortDesc, UserId, CreatedAt) values (@Name, @ShortDesc, @UserId, @CreatedAt)";

        await using var connection = await _context.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(command, product);
        
        return affectedRows;
    }
}