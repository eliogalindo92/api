using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Extensions 
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder) 
        {
            // --- Fixed IDs for Seeding ---
            const int adminRoleId = 1;
            const int userRoleId = 2;
            const int adminUserId = 1;
            const string adminPasswordHash = "$2a$11$PixMwN5Wo4vfx0RT9OauVuaLXAB6HgfhObEpMYJYwlN7ConG5UK6i"; 
            var seedDate = new DateTime(2025, 4, 11, 12, 0, 0, DateTimeKind.Utc);

            // --- 1. Seed Permissions ---
            var permReadUsers = new Permission { Id = 1, Denomination = "users.read", Description = "Read users", CreatedAt = seedDate };
            var permWriteUsers = new Permission { Id = 2, Denomination = "users.write", Description = "Create or Update users", CreatedAt = seedDate };
            var permDeleteUsers = new Permission { Id = 3, Denomination = "users.delete", Description = "Delete users", CreatedAt = seedDate };
            var permReadRoles = new Permission { Id = 4, Denomination = "roles.read", Description = "Read roles", CreatedAt = seedDate };
            var permWriteRoles = new Permission { Id = 5, Denomination = "roles.write", Description = "Create or Update roles", CreatedAt = seedDate };
            var permDeleteRoles = new Permission { Id = 6, Denomination = "roles.delete", Description = "Delete roles", CreatedAt = seedDate };

            modelBuilder.Entity<Permission>().HasData(
                permReadUsers,
                permWriteUsers,
                permDeleteUsers,
                permReadRoles,
                permWriteRoles,
                permDeleteRoles
            );

            // --- 2. Seed Roles ---
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = adminRoleId, Denomination = "Admin", Description = "System administrator", Enabled = true, CreatedAt = seedDate },
                new Role { Id = userRoleId, Denomination = "User", Description = "Standard user", Enabled = true, CreatedAt = seedDate }
            );

            // --- 3. Seed RolePermissions (Join table) ---
            modelBuilder.Entity<Role>()
                .HasMany(role => role.Permissions)
                .WithMany(permission => permission.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermissions",
                    join => join.HasOne<Permission>().WithMany().HasForeignKey("PermissionsId"),
                    join => join.HasOne<Role>().WithMany().HasForeignKey("RolesId"),
                    join =>
                    {
                        join.HasKey("RolesId", "PermissionsId");
                        join.ToTable("RolePermissions");
                        join.HasData(
                            new { RolesId = adminRoleId, PermissionsId = permReadUsers.Id },
                            new { RolesId = adminRoleId, PermissionsId = permWriteUsers.Id },
                            new { RolesId = adminRoleId, PermissionsId = permDeleteUsers.Id },
                            new { RolesId = adminRoleId, PermissionsId = permReadRoles.Id },
                            new { RolesId = adminRoleId, PermissionsId = permWriteRoles.Id },
                            new { RolesId = adminRoleId, PermissionsId = permDeleteRoles.Id }
                        );
                    });

            // --- 4. Seed Admin User ---
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminUserId,
                    FullName = "Main Administrator",
                    Username = "admin",
                    Email = "admin@domain.com",
                    Password = adminPasswordHash,
                    Status = "Enabled",
                    CreatedAt = seedDate
                }
            );

            // --- 5. Seed UserRoles (Join table) ---
            modelBuilder.Entity<User>()
               .HasMany(user => user.Roles)
               .WithMany(role => role.Users)
               .UsingEntity<Dictionary<string, object>>("UserRoles",
                   join => join.HasOne<Role>().WithMany().HasForeignKey("RolesId"),
                   join => join.HasOne<User>().WithMany().HasForeignKey("UsersId"),
                   join =>
                   {
                       join.HasKey("UsersId", "RolesId");
                       join.ToTable("UserRoles");
                       join.HasData(
                           new { UsersId = adminUserId, RolesId = adminRoleId }
                       );
                   });
        }
    }
}