using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using social_app_backend.Models;
using social_app_backend.Utils;

namespace social_app_backend.AppDataContext;

public class AppDbContext : DbContext
{
    private readonly DbSettings _dbSettings;
    public DbSet<User> Users { get; set; }
    public AppDbContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            _dbSettings.ConnectionString,
            new MySqlServerVersion(new Version(8, 0, 23))
        );
    }
}
