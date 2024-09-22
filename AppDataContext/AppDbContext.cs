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
    private readonly ILogger<AppDbContext> _logger;
    public AppDbContext(IOptions<DbSettings> dbSettings, ILogger<AppDbContext> logger)
    {
        _dbSettings = dbSettings.Value;
        _logger = logger;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        {
            optionsBuilder.UseMySql(
                _dbSettings.ConnectionString,
                ServerVersion.AutoDetect(_dbSettings.ConnectionString),
                    mysqlOptions =>
                    {
                        mysqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        );
                    }
            );
        }
        catch (Exception e)
        {
            _logger.LogError(e, "A database error occurred. " + e.Message);
        }

    }
}
