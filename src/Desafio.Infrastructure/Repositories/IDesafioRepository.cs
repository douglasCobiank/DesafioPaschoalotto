using Desafio.Infrastructure.Data.Models;

namespace Desafio.Infrastructure.Repositories
{
    public interface IDesafioRepository
    {
        Task AddAsync(DividaData entity);
        Task<DividaData?> GetByIdAsync(int id);
        Task<IEnumerable<DividaData>> GetAllAsync();
    }
}