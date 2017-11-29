using System.IO;
using DataAccessLayer.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ServiceLayer
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlite(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ServiceLayer"))
                .Options;

            return new MyDbContext(options);
        }
    }
}
