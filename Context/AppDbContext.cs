namespace api.Context;
using Models;
using Microsoft.EntityFrameworkCore;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<Permission> Permissions { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         // --- Fixed IDs for Seeding ---
            const int adminRoleId = 1;
            const int userRoleId = 2;
            const int adminUserId = 1; 
            const string adminPasswordHash = "$2a$11$PixMwN5Wo4vfx0RT9OauVuaLXAB6HgfhObEpMYJYwlN7ConG5UK6i";

            // --- 1. Seed Permissions ---
            var permReadUsers = new Permission { Id = 1, Denomination = "users.read", Description = "Read users",  CreatedAt = new DateTime(2025, 4, 10, 18, 0, 0, DateTimeKind.Utc) };
            var permWriteUsers = new Permission { Id = 2, Denomination = "users.write", Description = "Create or Update users",  CreatedAt = new DateTime(2025, 4, 10, 18, 0, 0, DateTimeKind.Utc) };
            var permDeleteUsers = new Permission { Id = 3, Denomination = "users.delete", Description = "Delete users",  CreatedAt = new DateTime(2025, 4, 10, 18, 0, 0, DateTimeKind.Utc) };
            var permReadRoles = new Permission { Id = 4, Denomination = "roles.read", Description = "Read roles",  CreatedAt = new DateTime(2025, 4, 10, 18, 0, 0, DateTimeKind.Utc) };
            var permWriteRoles = new Permission { Id = 5, Denomination = "roles.write", Description = "Create or Update roles",  CreatedAt = new DateTime(2025, 4, 10, 18, 0, 0, DateTimeKind.Utc) };
            var permDeleteRoles = new Permission { Id = 6, Denomination = "roles.delete", Description = "Delete roles", CreatedAt = new DateTime(2025, 4, 10, 18, 0, 0, DateTimeKind.Utc) };

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
                new Role { Id = adminRoleId, Denomination = "Admin", Description = "System administrator", Enabled = true, CreatedAt = new DateTime(2025, 4, 10, 18, 0, 0, DateTimeKind.Utc) },
                new Role { Id = userRoleId, Denomination = "User", Description = "Standard user", Enabled = true,  CreatedAt = new DateTime(2025, 4, 10, 18, 0, 0, DateTimeKind.Utc) }
            );

            // --- 3. Seed RolePermissions (Join table) ---
            // Using a dictionary to define the join table
            // This is a generic shape to define the join table for many-to-many relationships
            modelBuilder.Entity<Role>()
                .HasMany(role => role.Permissions)
                .WithMany(permission => permission.Roles)
                .UsingEntity<Dictionary<string, object>> ( // Generic shape to define join for HasData
                    "RolePermissions", // Join table name
                    join => join.HasOne<Permission>().WithMany().HasForeignKey("PermissionsId"),
                    join => join.HasOne<Role>().WithMany().HasForeignKey("RolesId"),
                    join =>
                    {
                        join.HasKey("RolesId", "PermissionsId"); // Define composite key
                        join.ToTable("RolePermissions"); // Secure table name
                        // Assign all permissions to the Admin role
                        join.HasData(
                            new { RolesId = adminRoleId, PermissionsId = permReadUsers.Id },
                            new { RolesId = adminRoleId, PermissionsId = permWriteUsers.Id },
                            new { RolesId = adminRoleId, PermissionsId = permDeleteUsers.Id },
                            new { RolesId = adminRoleId, PermissionsId = permReadRoles.Id },
                            new { RolesId = adminRoleId, PermissionsId = permWriteRoles.Id },
                            new { RolesId = userRoleId, PermissionsId = permDeleteRoles.Id }
                        
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
                    CreatedAt = new DateTime(2025, 4, 10, 18, 0, 0, DateTimeKind.Utc)
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
                        // Assign the Admin role to the Admin user
                        join.HasData(
                            new { UsersId = adminUserId, RolesId = adminRoleId }
                        );
                    });

        // modelBuilder.Entity<User>()
        //     .HasMany(user => user.Roles)
        //     .WithMany(role => role.Users)
        //     .UsingEntity(builder => builder.ToTable("UserRoles"));
        //
        // modelBuilder.Entity<Role>()
        //     .HasMany(role => role.Permissions)
        //     .WithMany(permission => permission.Roles)
        //     .UsingEntity(builder => builder.ToTable("RolePermissions"));
        
        modelBuilder.Entity<Role>()
            .HasIndex(role => role.Denomination)
            .IsUnique();

        modelBuilder.Entity<Permission>()
            .HasIndex(permission => permission.Denomination)
            .IsUnique();
    }
}