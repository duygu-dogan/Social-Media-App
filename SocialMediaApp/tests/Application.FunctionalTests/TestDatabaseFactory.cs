using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Application.FunctionalTests;
public class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddProvider(new NUnitLoggerProvider(LogLevel.Information));
        });
        var logger = loggerFactory.CreateLogger<PostgreSqlTestDatabase>();

        var database = new PostgreSqlTestDatabase(logger);

        await database.InitialiseAsync();

        return database;
    }
}
