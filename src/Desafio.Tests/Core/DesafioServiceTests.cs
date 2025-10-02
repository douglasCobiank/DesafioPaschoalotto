using AutoMapper;
using Desafio.Core.Handler.Models;
using Desafio.Core.Handler.Services;
using Desafio.Infrastructure.Data.Models;
using Desafio.Infrastructure.Repositories;
using Moq;

namespace Desafio.Tests.Core
{
    public class DesafioServiceTests
    {
        private readonly Mock<IDesafioRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly DesafioService _service;

        public DesafioServiceTests()
        {
            _mockRepo = new Mock<IDesafioRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DividaDto, DividaData>().ReverseMap();
                cfg.CreateMap<ParcelaDto, ParcelaData>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _service = new DesafioService(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task AddDividaAsync_DeveDefinirDataVencimentoComoUtc()
        {
            // Arrange
            var dividaDto = new DividaDto
            {
                Parcelas = new List<ParcelaDto>
                {
                    new ParcelaDto
                    {
                        ValorParcela = 200,
                        DataVencimento = new DateTime(2025, 10, 01, 12, 0, 0, DateTimeKind.Unspecified)
                    }
                }
            };

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<DividaData>()))
                    .Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddDividaAsync(dividaDto);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<DividaData>()), Times.Once);
            Assert.NotNull(result);
            Assert.Single(result.Parcelas);

            var parcela = result.Parcelas[0];
            Assert.Equal(DateTimeKind.Utc, parcela.DataVencimento.Kind); // garante que está como UTC
            Assert.Equal(200, parcela.ValorParcela);
        }
    }
}