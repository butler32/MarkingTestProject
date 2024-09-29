using System.Windows.Controls;
using MarkingTestProject.Interfaces;
using MarkingTestProject.Models;
using MarkingTestProject.Utilities.Interfaces;
using MarkingTestProject.Views;
using Microsoft.Extensions.DependencyInjection;

namespace MarkingTestProject.ViewModels
{
    public class NavigationViewModel : BaseViewModel
    {
        private IServiceProvider _serviceProvider;

        private CurrentTaskModel _currentTaskModel;

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand CurrentTaskCommand { get; }
        public ICommand ProductCommand { get; }
        public ICommand BoxCommand { get; }
        public ICommand PalletCommand { get; }

        private void SetCurrentTaskView()
        {
            CurrentView = _serviceProvider.GetRequiredService<CurrentTaskView>();
        }

        private void SetProductView()
        {
            CurrentView = _serviceProvider.GetRequiredService<ProductView>();
        }

        private void SetBoxView()
        {
            CurrentView = _serviceProvider.GetRequiredService<BoxView>();
        }

        private void SetPalletView()
        {
            CurrentView = _serviceProvider.GetRequiredService<PalletView>();
        }

        public NavigationViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            CurrentTaskCommand = ICommand.From(SetCurrentTaskView);
            ProductCommand = ICommand.From(SetProductView);
            BoxCommand = ICommand.From(SetBoxView);
            PalletCommand = ICommand.From(SetPalletView);

            SetCurrentTaskView();
        }
    }
}
