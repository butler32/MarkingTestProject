using MarkingTestProject.Domain.Entities;

namespace MarkingTestProject.Infrastructure.Interfaces
{
    public interface IBottleRepository<T> : IBaseRepository<T> 
        where T : Bottle
    {
        Task<IEnumerable<Bottle>> GetAllByGtinAsync(string gtin, CancellationToken cancellationToken);
    }
}
