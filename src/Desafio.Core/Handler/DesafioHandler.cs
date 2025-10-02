using Desafio.Core.Handler.Interface;
using Desafio.Core.Handler.Models;

namespace Desafio.Core.Handler
{
    public class DesafioHandler(IDesafioService dividaService) : IDesafioHandler
    {
        private readonly IDesafioService _dividaService = dividaService;

        public async Task CadastraDividaHandler(DividaDto dividaDto)
        {
            await _dividaService.AddDividaAsync(dividaDto);
        }

        public async Task<List<DividaDto>> BuscarDividaHandler()
        {
            return await _dividaService.GetDividaAsync();
        }

        public async Task<DividaDto> BuscarDividaPorIdHandler(int idDivida)
        {
            return await _dividaService.GetByIdDividaAsync(idDivida);
        }
    }
}