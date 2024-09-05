//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using SocialMediaApp.Infrastructure.Data.Configurations;

//namespace SocialMediaApp.Infrastructure.Data;
//public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//{
//    public ApplicationDbContext CreateDbContext(string[] args)
//    {
//        var configuration = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.json")
//            .Build();
//        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//        var connectionString = configuration.GetConnectionString("PostgresConnection");
//        if (string.IsNullOrEmpty(connectionString))
//        {
//            throw new InvalidOperationException("Connection string 'PostgresConnection' not found.");
//        }
//        optionsBuilder.UseNpgsql(connectionString);

//        return new ApplicationDbContext(optionsBuilder.Options);
//    }

//}

