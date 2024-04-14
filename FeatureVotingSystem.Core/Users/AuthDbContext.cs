using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FeatureVotingSystem.Core.Users;

public class AuthDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserEntity>().ToTable("Users");
        builder.Entity<IdentityRole<int>>().ToTable("Roles");
        builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
        builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
        builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

        InsertSeedData(builder);

        builder.Entity<UserEntity>()
            .Property(p => p.UserName)
            .HasColumnName("Name");
    }

    private void InsertSeedData(ModelBuilder builder)
    {
        builder.Entity<IdentityRole<int>>().HasData(new[]
        {
            new IdentityRole<int> { Id = 1, Name = "admin", NormalizedName = "ADMIN" },
            new IdentityRole<int> { Id = 2, Name = "user", NormalizedName = "USER" }
        });

        var email = "Admin@adminmail.com";
        var password = "AdminStrongPassword1";
        var adminUser = new UserEntity
        {
            Id = 1,
            Email = email,
            UserName = "Jhon",
            Surname = "Doe"
        };

        var hasher = new PasswordHasher<UserEntity>();
        adminUser.PasswordHash = hasher.HashPassword(adminUser, password);
        builder.Entity<UserEntity>().HasData(adminUser);

        builder.Entity<IdentityUserRole<int>>().HasData(new[]
        {
            new IdentityUserRole<int> { UserId = 1, RoleId = 1 }
        });
    }
}