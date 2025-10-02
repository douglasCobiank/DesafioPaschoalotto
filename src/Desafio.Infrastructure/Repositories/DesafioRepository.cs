using System.Linq.Expressions;
using Desafio.Infrastructure.Data;
using Desafio.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Infrastructure.Repositories
{
    public class DesafioRepository(DesafioDbContext context) : IDesafioRepository
    {
        protected readonly DesafioDbContext _context = context;
        protected readonly DbSet<DividaData> _dbSet = context.Set<DividaData>();

        public async Task AddAsync(DividaData entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<DividaData?> GetByIdAsync(int id)
        {
            var result = await _dbSet.Include(d => d.Parcelas).ToListAsync();
            
            return result.Find(p => p.Id == id);
        }

        public async Task<IEnumerable<DividaData>> GetAllAsync()
        {
            return await _dbSet.Include(d => d.Parcelas).ToListAsync();
        }

        public async Task<IEnumerable<DividaData>> FindAsync(Expression<Func<DividaData, bool>> predicate)
        {
            var result = await _dbSet.Include(d => d.Parcelas).Where(predicate).ToListAsync();
            return result;
        }

    }
}