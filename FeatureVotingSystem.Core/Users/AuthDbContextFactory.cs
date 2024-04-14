using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FeatureVotingSystem.Core.Users;

internal class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
{
    public AuthDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<AuthDbContext>();
        builder.UseSqlServer(configuration.GetConnectionString(Environment.UserName),
            b => b.MigrationsAssembly("FeatureVotingSystem.Core"));
        return new AuthDbContext(builder.Options);
    }
}