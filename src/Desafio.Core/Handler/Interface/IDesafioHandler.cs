using Desafio.Core.Handler.Models;

namespace Desafio.Core.Handler.Interface
{
    public interface IDesafioHandler
    {
        Task CadastraDividaHandler(DividaDto dividaDto);
        Task<List<DividaDto>> BuscarDividaHandler();
        Task<DividaDto> BuscarDividaPorIdHandler(int idDivida);
    }
}