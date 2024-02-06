using Microsoft.EntityFrameworkCore;

namespace SocialMediaApp.Database;

public class DatabaseContext : DbContext
{
   private readonly string _databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? throw new InvalidOperationException();
   
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    // public required DbSet<User> Users { get; set; }
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_databaseUrl);
    }
}