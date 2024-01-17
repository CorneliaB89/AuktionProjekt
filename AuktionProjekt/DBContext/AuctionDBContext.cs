using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

public class AuctionDbContext : DbContext
{
    private readonly string _connectionString;

    public AuctionDbContext()
    {
        // ladda från appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

      // Läs ifrån connection string från appsettings.json
        _connectionString = configuration.GetConnectionString("AuctionDbConnection");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}
