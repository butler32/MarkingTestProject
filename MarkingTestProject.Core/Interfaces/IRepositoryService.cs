using MarkingTestProject.Core.DTOS;

namespace MarkingTestProject.Core.Interfaces
{
    public interface IRepositoryService
    {
        Task Import(IEnumerable<PalletDTO> pallets, CancellationToken cancellationToken);
        Task DeleteAll(CancellationToken cancellationToken);
        Task<IEnumerable<PalletDTO>> GetAllPalletsByGtin(string gtin, CancellationToken cancellationToken);
        Task<IEnumerable<BoxDTO>> GetAllBoxesByGtin(string gtin, CancellationToken cancellationToken);
        Task<IEnumerable<BottleDTO>> GetAllBottlesByGtin(string gtin, CancellationToken cancellationToken);
        Task<IEnumerable<PalletDTO>> GetAllDataByGtinAsync(string gtin, CancellationToken cancellationToken);
    }
}
