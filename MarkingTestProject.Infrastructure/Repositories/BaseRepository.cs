using MarkingTestProject.Domain.Entities;
using MarkingTestProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarkingTestProject.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        protected readonly TestProjDbContext _context;

        public BaseRepository(TestProjDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }

            return entity;
        }

        public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<T?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync((i => i.Id == id), cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task DeleteAllAsync(CancellationToken cancellationToken)
        {
            await _context.Set<T>().ExecuteDeleteAsync();
        }
    }
}
