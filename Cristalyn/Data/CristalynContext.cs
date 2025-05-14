using Microsoft.EntityFrameworkCore;
using Cristalyn.Models;

namespace Cristalyn.Data
{
    public class CristalynContext : DbContext
    {
        public CristalynContext(DbContextOptions<CristalynContext> options)
            : base(options)
        {
        }

        public DbSet<Utilizator> Utilizatori { get; set; }
        public DbSet<Produs> Produse { get; set; }
        public DbSet<Comanda> Comenzi { get; set; }
        //public DbSet<ComandaProdus> ComandaProduse { get; set; }
    }
}
