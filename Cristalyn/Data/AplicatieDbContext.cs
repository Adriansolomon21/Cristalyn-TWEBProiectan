using Microsoft.EntityFrameworkCore;
using Cristalyn.Models;
using System.Collections.Generic;

namespace Cristalyn.Data
{
    public class AplicatieDbContext : DbContext
    {
        public AplicatieDbContext(DbContextOptions<AplicatieDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cristalyn.Models.Utilizator> Utilizatori { get; set; }

        public DbSet<Comanda> Comenzi { get; set; }
        public DbSet<CosItem> CosItems { get; set; } // doar dacă vrei să accesezi separat

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comanda>()
                .HasMany(c => c.Produse)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
