using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Entities;

namespace SocialMediaApp.Database;

public class DatabaseContext : DbContext
{
    private readonly string _databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? throw new InvalidOperationException();

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public required DbSet<User> Users { get; set; }
    public required DbSet<Post> Posts { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_databaseUrl);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}