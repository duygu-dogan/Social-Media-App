using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialMediaApp.Infrastructure.Data.Configurations;

namespace SocialMediaApp.Infrastructure.Data;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString);

        // IUserNotificationService bağımlılığı olmadan migration işlemi gerçekleşebilir
        return new ApplicationDbContext(optionsBuilder.Options, null);
    }

}

