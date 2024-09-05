using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SocialMediaApp.Infrastructure.Data.Configurations;
static class Configuration
{
    public static string GetConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Web"));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("PostgresConnection")!;
        }
    }
}
