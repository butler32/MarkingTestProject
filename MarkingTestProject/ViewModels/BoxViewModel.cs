using MarkingTestProject.Core.DTOS;
using MarkingTestProject.Interfaces;
using MarkingTestProject.Models;
using MarkingTestProject.Utilities.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace MarkingTestProject.ViewModels
{
    public class BoxViewModel : BaseViewModel
    {
        private IDataBaseService _dataBaseService;
        private CurrentTaskModel _currentTask;

        private ObservableCollection<BoxDTO> boxes;
        public ObservableCollection<BoxDTO> Boxes
        {
            get => boxes;
            set { boxes = value; OnPropertyChanged(); }
        }

        public ICommand GetDataCommand { get; }

        private async Task GetData()
        {
            try
            {
                var data = await _dataBaseService.GetAllBoxesData(_currentTask.Gtin);

                Boxes = new ObservableCollection<BoxDTO>(data.ToList().OrderBy(x => x.Id));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public BoxViewModel(IDataBaseService dataBaseService, CurrentTaskModel currentTask)
        {
            _currentTask = currentTask;
            _dataBaseService = dataBaseService;

            GetDataCommand = ICommand.From(GetData);
            GetDataCommand.Execute(null);
        }
    }
}
