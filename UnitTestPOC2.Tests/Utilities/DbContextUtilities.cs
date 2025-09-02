using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace UnitTestPOC2.Tests.Utilities
{
    public static class DbContextUtilities
    {
        public static AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            // Ensure the database is created
            context.Database.EnsureCreated();

            return context;
        }
    }
}