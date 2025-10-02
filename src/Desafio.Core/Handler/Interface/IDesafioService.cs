using Desafio.Core.Handler.Models;

namespace Desafio.Core.Handler.Interface
{
    public interface IDesafioService
    {
        Task AddDividaAsync(DividaDto dividaDto);
        Task<List<DividaDto>> GetDividaAsync();
        Task<DividaDto> GetByIdDividaAsync(int idDivida);
    }
}