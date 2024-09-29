namespace MarkingTestProject.Interfaces
{
    public interface ICancellationTokenService
    {
        CancellationToken GetToken();
        void Cancel();
    }
}
