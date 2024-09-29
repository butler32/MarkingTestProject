using MarkingTestProject.Utilities.Interfaces;

namespace MarkingTestProject.Utilities
{
    public class RelayCommand : ICommand
    {
        private readonly Action? _action;
        private readonly Func<Task>? _asyncAction;

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public RelayCommand(Func<Task> asyncAction)
        {
            _asyncAction = asyncAction;
        }

        public event EventHandler? CanExecuteChanged;

        public async Task ExecuteAsync()
        {
            if (_action != null)
            {
                _action.Invoke();
            }
            else if (_asyncAction != null)
            {
                await _asyncAction.Invoke();
            }
        }
    }
}
