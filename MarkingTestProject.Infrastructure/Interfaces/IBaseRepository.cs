using MarkingTestProject.Domain.Entities;

namespace MarkingTestProject.Infrastructure.Interfaces
{
    public interface IBaseRepository <T> 
        where T : BaseEntity
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);
        Task<T?> GetAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAllAsync(CancellationToken cancellationToken);
    }
}
