using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using Respawn;
using SocialMediaApp.Infrastructure.Data;
using Testcontainers.PostgreSql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.FunctionalTests;
public class PostgreSqlTestDatabase : ITestDatabase
{
    private readonly PostgreSqlContainer _container;
    private DbConnection _connection = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;
    private readonly ILogger<PostgreSqlTestDatabase> _logger;
    private readonly IConfiguration _configuration;

    public PostgreSqlTestDatabase(ILogger<PostgreSqlTestDatabase> logger)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.test.json");
        
        _configuration = configurationBuilder.Build();

        _container = new PostgreSqlBuilder()
            .WithAutoRemove(true)
            .Build();
        _logger = logger;
    }

    public async Task InitialiseAsync()
    {
        await _container.StartAsync();

        _connectionString = _configuration.GetConnectionString("PostgresConnection")!;

        _logger.LogInformation("Updated Connection String: {ConnectionString}", _connectionString);

        _connection = new NpgsqlConnection(_connectionString);
        await _connection.OpenAsync();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connectionString)
            .Options;
        var context = new ApplicationDbContext(options);

        context.Database.Migrate();

        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        });
    }
    public DbConnection GetConnection()
    {
        return _connection;
    }
    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connection);
    }
    public async Task DisposeAsync()
    {
       await _connection.DisposeAsync();
        _connection.Close();
       await _container.DisposeAsync();
    }

}
