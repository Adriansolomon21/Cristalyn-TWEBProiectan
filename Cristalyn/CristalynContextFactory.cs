using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Cristalyn.Data;

namespace Cristalyn
{
    public class CristalynContextFactory : IDesignTimeDbContextFactory<CristalynContext>
    {
        public CristalynContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CristalynContext>();

            // Aici setezi conexiunea ta locală
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CristalynDB;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new CristalynContext(optionsBuilder.Options);
        }
    }
}
    