using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptographer
{
    class AppContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }

        public AppContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LicenseKey>().HasKey(lk => new { lk.ClientId, lk.ProductId });

            modelBuilder.Entity<LicenseKey>().HasOne(lk => lk.Client)
                                             .WithMany(c => c.LicenseKeys)
                                             .HasForeignKey(lk => lk.ClientId);

            modelBuilder.Entity<LicenseKey>().HasOne(lk => lk.Product)
                                             .WithMany(p => p.LicenseKeys)
                                             .HasForeignKey(lk => lk.ProductId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DbLocal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
