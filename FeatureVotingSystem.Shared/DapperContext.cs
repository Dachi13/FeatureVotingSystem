using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FeatureVotingSystem.Shared;

public class DapperContext : IDisposable
{
    private readonly string _connectionString;
    private SqlConnection _connection;

    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString(Environment.UserName)!;
    }

    public async Task<SqlConnection> CreateConnectionAsync()
    {
        _connection = new SqlConnection(_connectionString);
        await _connection.OpenAsync();

        return _connection;
    }

    public void Dispose()
    {
        _connection?.CloseAsync();
    }
}