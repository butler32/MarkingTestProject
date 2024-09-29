using MarkingTestProject.Domain.Entities;

namespace MarkingTestProject.Infrastructure.Interfaces
{
    public interface IBoxRepository<T> : IBaseRepository<T>
        where T : Box
    {
        Task<IEnumerable<Box>> GetAllByGtinAsync(string gtin, CancellationToken cancellationToken);
    }
}
