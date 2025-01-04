using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SAV.Models;

namespace SAV.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Reclamation> Reclamations { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<Technicien> Techniciens { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<PieceRechange> PiecesRechange { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important for Identity configuration

            // Configure composite key for IdentityUserLogin
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(i => new { i.LoginProvider, i.ProviderKey });

            // Decimal precision for specific properties
            modelBuilder.Entity<Article>()
                .Property(a => a.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Intervention>()
                .Property(i => i.TotalCost)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<InterventionHistory>()
                .Property(ih => ih.Cost)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PieceRechange>()
                .Property(pr => pr.Price)
                .HasColumnType("decimal(18, 2)");

            // Define relationship between Reclamation and Client
            modelBuilder.Entity<Reclamation>()
                .HasOne(r => r.Client)
                .WithMany()  // A Client can have many Reclamations
                .HasForeignKey(r => r.ClientId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data for PiecesRechange
            modelBuilder.Entity<PieceRechange>().HasData(
                new PieceRechange { Id = 3, Name = "Battery", Price = 49.99m },
                new PieceRechange { Id = 4, Name = "Screen", Price = 129.99m },
                new PieceRechange { Id = 5, Name = "Hard Drive", Price = 79.99m },
                new PieceRechange { Id = 6, Name = "RAM Module", Price = 59.99m }
            );

            // Seed data for Techniciens
            modelBuilder.Entity<Technicien>().HasData(
                new Technicien { Id = 3, Name = "John Doe", Phone = "123-456-7890" },
                new Technicien { Id = 4, Name = "Jane Smith", Phone = "987-654-3210" },
                new Technicien { Id = 5, Name = "Alex Johnson", Phone = "555-123-4567" }
            );
        }
    }
}
