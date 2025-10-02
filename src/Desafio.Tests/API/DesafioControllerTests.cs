using AutoMapper;
using Desafio.API.Controllers;
using Desafio.API.Models;
using Desafio.Core.Handler.Interface;
using Desafio.Core.Handler.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Desafio.Tests.API
{
    public class DesafioControllerTests
    {
        private readonly Mock<IDesafioHandler> _mockHandler;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DesafioController _controller;

        public DesafioControllerTests()
        {
            _mockHandler = new Mock<IDesafioHandler>();
            _mockMapper = new Mock<IMapper>();
            _controller = new DesafioController(_mockHandler.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task AdicionaDivida_DeveRetornarOk()
        {
            // Arrange
            var divida = new Divida { NumeroTitulo = 123, NomeDevedor = "Douglas" };
            var dividaDto = new DividaDto { NumeroTitulo = 123, NomeDevedor = "Douglas" };

            _mockMapper.Setup(m => m.Map<DividaDto>(It.IsAny<Divida>()))
                       .Returns(dividaDto);

            _mockHandler.Setup(h => h.CadastraDividaHandler(It.IsAny<DividaDto>()))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AdicionaDivida(divida);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<IEnumerable<string>>(okResult.Value);
            Assert.Contains("Divida cadastrada", response);
        }

        [Fact]
        public async Task GetAll_ComDividas_DeveRetornarOk()
        {
            // Arrange
            var dividas = new List<DividaDto>
            {
                new DividaDto { NumeroTitulo = 1, NomeDevedor = "Teste" }
            };

            _mockHandler.Setup(h => h.BuscarDividaHandler())
                        .ReturnsAsync(dividas);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<List<DividaDto>>(okResult.Value);
            Assert.Single(response);
        }

        [Fact]
        public async Task GetAll_SemDividas_DeveRetornarBadRequest()
        {
            // Arrange
            _mockHandler.Setup(h => h.BuscarDividaHandler())
                        .ReturnsAsync(new List<DividaDto>());

            // Act
            var result = await _controller.GetAll();

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Não foi encontrada nenhuma divida", badResult.Value);
        }

        [Fact]
        public async Task GetById_Encontrada_DeveRetornarOk()
        {
            // Arrange
            var divida = new DividaDto { NumeroTitulo = 10, NomeDevedor = "Douglas" };

            _mockHandler.Setup(h => h.BuscarDividaPorIdHandler(10))
                        .ReturnsAsync(divida);

            // Act
            var result = await _controller.GetById(10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<DividaDto>(okResult.Value);
            Assert.Equal(10, response.NumeroTitulo);
        }

        [Fact]
        public async Task GetById_NaoEncontrada_DeveRetornarBadRequest()
        {
            // Arrange
            _mockHandler.Setup(h => h.BuscarDividaPorIdHandler(It.IsAny<int>()))
                        .ReturnsAsync((DividaDto?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Não foi encontrada nenhuma divida", badResult.Value);
        }
    }
}