using MarkingTestProject.Core.DTOS;
using MarkingTestProject.Domain.Entities;
using MarkingTestProject.Interfaces;
using MarkingTestProject.Models;
using MarkingTestProject.Utilities.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace MarkingTestProject.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private IDataBaseService _dataBaseService;
        private CurrentTaskModel _currentTask;

        private ObservableCollection<BottleDTO> bottles;
        public ObservableCollection<BottleDTO> Bottles
        {
            get => bottles;
            set { bottles = value; OnPropertyChanged(); }
        }

        public ICommand GetDataCommand { get; }

        private async Task GetData()
        {
            try
            {
                var data = await _dataBaseService.GetAllBottlesData(_currentTask.Gtin);
                
                Bottles = new ObservableCollection<BottleDTO>(data.ToList().OrderBy(x => x.Id));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ProductViewModel(IDataBaseService dataBaseService, CurrentTaskModel currentTask)
        {
            _currentTask = currentTask;
            _dataBaseService = dataBaseService;

            GetDataCommand = ICommand.From(GetData);
            GetDataCommand.Execute(null);
        }
    }
}
