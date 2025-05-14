using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cristalyn.Data
{
    public class AplicatieDbContextFactory : IDesignTimeDbContextFactory<AplicatieDbContext>
    {
        public AplicatieDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AplicatieDbContext>();

            // ATENȚIE: scrie direct connection string-ul aici (fără appsettings.json)
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CristalynDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new AplicatieDbContext(optionsBuilder.Options);
        }
    }
}

