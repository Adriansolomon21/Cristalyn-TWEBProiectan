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
        public DbSet<Cupon> Cupoane { get; set; }
        public DbSet<PuncteFidelitate> PuncteFidelitate { get; set; }
        public DbSet<ReducereCategorie> ReduceriCategorii { get; set; }
        //public DbSet<ComandaProdus> ComandaProduse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurare pentru PuncteFidelitate
            modelBuilder.Entity<PuncteFidelitate>()
                .HasIndex(p => p.EmailUtilizator)
                .IsUnique();

            // Configurare pentru ReducereCategorie
            modelBuilder.Entity<ReducereCategorie>()
                .HasIndex(r => r.Categorie);
        }
    }
}
