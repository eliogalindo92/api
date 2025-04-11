using api.Extensions;

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
        // Calls the extension seed method
        modelBuilder.Seed(); 
             
        modelBuilder.Entity<Role>()
            .HasIndex(role => role.Denomination)
            .IsUnique();

        modelBuilder.Entity<Permission>()
            .HasIndex(permission => permission.Denomination)
            .IsUnique();
    }
}