using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Desafio.Infrastructure.Data
{
    public class CheerDbContextFactory: IDesignTimeDbContextFactory<DesafioDbContext>
    {
        public DesafioDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DesafioDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=desafiodb;Username=postgres;Password=postgres;KeepAlive=30");

            return new DesafioDbContext(optionsBuilder.Options);
        }
    }
}