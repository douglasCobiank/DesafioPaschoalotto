using Desafio.Infrastructure.Data;
using Desafio.Infrastructure.Data.Models;
using Desafio.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Tests.Infrastructure
{
    public class DesafioRepositoryTests
    {
        private DesafioDbContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<DesafioDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // banco único por teste
                .Options;

            return new DesafioDbContext(options);
        }

        [Fact]
        public async Task AddAsync_DeveAdicionarDividaNoBanco()
        {
            // Arrange
            var context = CriarContextoInMemory();
            var repo = new DesafioRepository(context);

            var divida = new DividaData
            {
                Id = 1,
                Parcelas = new List<ParcelaData>
                {
                    new ParcelaData { Id = 100, ValorParcela = 300, DataVencimento = DateTime.UtcNow }
                }
            };

            // Act
            await repo.AddAsync(divida);
            var result = await context.Dividas.Include(d => d.Parcelas).FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Single(result.Parcelas);
            Assert.Equal(300, result.Parcelas.First().ValorParcela);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarDividaPorId()
        {
            var context = CriarContextoInMemory();
            var repo = new DesafioRepository(context);

            var divida = new DividaData { Id = 2, Parcelas = new List<ParcelaData>() };
            await repo.AddAsync(divida);

            var result = await repo.GetByIdAsync(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTodasAsDividas()
        {
            var context = CriarContextoInMemory();
            var repo = new DesafioRepository(context);

            await repo.AddAsync(new DividaData { Id = 3 });
            await repo.AddAsync(new DividaData { Id = 4 });

            var result = await repo.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task FindAsync_DeveFiltrarDividasCorretamente()
        {
            var context = CriarContextoInMemory();
            var repo = new DesafioRepository(context);

            await repo.AddAsync(new DividaData { Id = 5 });
            await repo.AddAsync(new DividaData { Id = 6 });

            var result = await repo.FindAsync(d => d.Id == 6);

            Assert.Single(result);
            Assert.Equal(6, result.First().Id);
        }
    }
}