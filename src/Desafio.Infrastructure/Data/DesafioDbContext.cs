using Desafio.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Infrastructure.Data
{
    public class DesafioDbContext : DbContext
    {
        public DesafioDbContext(DbContextOptions<DesafioDbContext> options)
            : base(options) { }

        public DbSet<DividaData> Dividas { get; set; }
        public DbSet<ParcelaData> Parcelas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // garante explicitamente a PK (útil se o nome for diferente)
            modelBuilder.Entity<DividaData>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ParcelaData>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.DataVencimento).HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}