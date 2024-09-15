using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FunctionalTests
{
    public interface ITestDatabase
    {
        Task InitialiseAsync();
        DbConnection GetConnection();
        Task ResetAsync();
        Task DisposeAsync();
    }
}
