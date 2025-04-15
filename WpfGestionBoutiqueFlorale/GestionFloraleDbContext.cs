using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WpfGestionBoutiqueFlorale.Models;

namespace WpfGestionBoutiqueFlorale
{
    class GestionFloraleDbContext: DbContext
    {
        public DbSet<Bouquet> Bouquets { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Fleur> Fleurs { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string database_name = "GestionFloraleDb";

            dbContextOptionsBuilder.UseSqlServer($"{connection_string};Database={database_name};");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fleur>()
                .HasOne(f => f.Commande)
                .WithMany(c => c.Fleurs)
                .HasForeignKey(f => f.IdCommande)
                .OnDelete(DeleteBehavior.NoAction); // 🔧 Empêche le chemin multiple

            modelBuilder.Entity<Bouquet>()
                .HasOne(b => b.Commande)
                .WithMany(c => c.Bouquets)
                .HasForeignKey(b => b.IdCommande)
                .OnDelete(DeleteBehavior.Cascade); // ou NoAction si besoin
        }


    }
}
