using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var superAdminRoleId = "3763d875-9c40-45e7-8a75-0dbbde9c8155";
        var adminRoleId = "nJsIs8GyOEe5vO6wJt9Guw%3d%3d";
        var userRoleId = "ae81af46-54e8-477c-9171-cf58916c82a2";

        // Seed Roles (User, Admin, Super Admin)
        var roles = new List<IdentityRole>
        {
            new IdentityRole()
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = superAdminRoleId,
                ConcurrencyStamp = superAdminRoleId
            },
            new IdentityRole()
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            },
            new IdentityRole()
            {
                Name = "User",
                NormalizedName = "User",
                Id = userRoleId,
                ConcurrencyStamp = userRoleId
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);

        // Seed Super Admin User
        var superAdminId = "03bf295d-7983-4702-aa8d-6aa18cbc8f27";
        var superAdminUser = new IdentityUser()
        {
            Id = superAdminId,
            UserName = "superadmin@bloggie.com",
            Email = "superadmin@bloggie.com",
            NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
            NormalizedUserName = "superadmin@bloggie.com".ToUpper()
        };

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(superAdminUser, "superadmin123");

        builder.Entity<IdentityUser>().HasData(superAdminUser);

        // Add All Roles To Super Admin User
        var superAdminRoles = new List<IdentityUserRole<string>>()
        {
            new IdentityUserRole<string>()
            {
                RoleId = superAdminRoleId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>()
            {
                RoleId = adminRoleId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>()
            {
                RoleId = userRoleId,
                UserId = superAdminId
            }
        };

        builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
    }
}