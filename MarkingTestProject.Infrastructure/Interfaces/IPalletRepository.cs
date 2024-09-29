using MarkingTestProject.Domain.Entities;

namespace MarkingTestProject.Infrastructure.Interfaces
{
    public interface IPalletRepository<T> : IBaseRepository<T>
        where T : Pallet
    {
        Task<IEnumerable<Pallet>> GetAllByGtinAsync(string gtin, CancellationToken cancellationToken);
        Task<IEnumerable<Pallet>> GetAllWithReferencesByGtinAsync(string gtin, CancellationToken cancellationToken);
    }
}
