using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace Application.FunctionalTests
{
    public interface ITestDatabase
    {
        string GetConnectionString();
        Task InitialiseAsync();
        DbConnection GetConnection();
        Task ResetAsync();
        Task DisposeAsync();
    }
}
