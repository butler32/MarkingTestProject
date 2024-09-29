using MarkingTestProject.Models;

namespace MarkingTestProject.Interfaces
{
    public interface IAPIService : IDisposable
    {
        CurrentTaskModel GetData();
    }
}
