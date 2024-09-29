using MarkingTestProject.Core.DTOS;
using MarkingTestProject.Models;

namespace MarkingTestProject.Interfaces
{
    public interface IDataBaseService
    {
        Task<bool> ReadFile(string filePath, string targetGTIN, CurrentTaskModel currentTaskModel);
        Task<IEnumerable<PalletDTO>> GetAllPalletsData(string gtin);
        Task<IEnumerable<BottleDTO>> GetAllBottlesData(string gtin);
        Task<IEnumerable<BoxDTO>> GetAllBoxesData(string gtin);
        Task<IEnumerable<PalletDTO>> GetAllData(string gtin);
    }
}
