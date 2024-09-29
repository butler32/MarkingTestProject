using MarkingTestProject.Interfaces;

namespace MarkingTestProject.Services
{
    class CancellationTokenService : ICancellationTokenService
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public CancellationToken GetToken()
        {
            return _cts.Token;
        }

        public void Cancel()
        {
            _cts.Cancel();
        }
    }
}
