using Desafio.Core.Handler;
using Desafio.Core.Handler.Interface;
using Desafio.Core.Handler.Models;
using Moq;

namespace Desafio.Tests.Core
{
    public class DesafioHandlerTests
    {
        private readonly Mock<IDesafioService> _mockService;
        private readonly DesafioHandler _handler;

        public DesafioHandlerTests()
        {
            _mockService = new Mock<IDesafioService>();
            _handler = new DesafioHandler(_mockService.Object);
        }

        [Fact]
        public async Task CadastraDividaHandler_DeveChamarServico()
        {
            // Arrange
            var divida = new DividaDto { NumeroTitulo = 123, NomeDevedor = "Douglas" };

            _mockService.Setup(s => s.AddDividaAsync(divida))
                        .Returns(Task.FromResult(divida));

            // Act
            await _handler.CadastraDividaHandler(divida);

            // Assert
            _mockService.Verify(s => s.AddDividaAsync(divida), Times.Once);
        }

        [Fact]
        public async Task BuscarDividaHandler_DeveRetornarLista()
        {
            // Arrange
            var lista = new List<DividaDto>
            {
                new DividaDto { NumeroTitulo = 1, NomeDevedor = "Teste" }
            };

            _mockService.Setup(s => s.GetDividaAsync())
                        .ReturnsAsync(lista);

            // Act
            var result = await _handler.BuscarDividaHandler();

            // Assert
            Assert.Equal(lista, result);
            _mockService.Verify(s => s.GetDividaAsync(), Times.Once);
        }

        [Fact]
        public async Task BuscarDividaPorIdHandler_DeveRetornarDivida()
        {
            // Arrange
            var divida = new DividaDto { NumeroTitulo = 10, NomeDevedor = "Douglas" };

            _mockService.Setup(s => s.GetByIdDividaAsync(10))
                        .ReturnsAsync(divida);

            // Act
            var result = await _handler.BuscarDividaPorIdHandler(10);

            // Assert
            Assert.Equal(divida, result);
            _mockService.Verify(s => s.GetByIdDividaAsync(10), Times.Once);
        }
    }
}