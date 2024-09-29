using MarkingTestProject.Interfaces;
using MarkingTestProject.Models;
using MarkingTestProject.Utilities.Interfaces;
using Microsoft.Win32;
using System.Windows;

namespace MarkingTestProject.ViewModels
{
    public class CurrentTaskViewModel : BaseViewModel
    {
        private IDataBaseService _dataBaseService;

        private CurrentTaskModel _currentTask;
        public CurrentTaskModel CurrentTask
        {
            get => _currentTask;
            set { _currentTask = value; OnPropertyChanged(); }
        }

        private string _filePath;

        public void SetCurrentTask(CurrentTaskModel currentTask)
        {
            _currentTask = currentTask;
        }

        public ICommand DialogCommand { get; }

        private async Task OpenDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt"
            };

            if (fileDialog.ShowDialog() == true)
            {
                if (fileDialog.FileName.Split('.').Last() == "txt")
                {
                    var isDone = await _dataBaseService.ReadFile(fileDialog.FileName, CurrentTask.Gtin, CurrentTask);

                    if (isDone)
                    {
                        MessageBox.Show("File read successfully");
                    }
                    else
                    {
                        MessageBox.Show("Something wrong");
                    }
                }
                else
                {
                    MessageBox.Show("File must be a valid Text file");
                }
            }
        }

        public CurrentTaskViewModel(IDataBaseService dataBaseService, CurrentTaskModel currentTask)
        {
            CurrentTask = currentTask;
            _dataBaseService = dataBaseService;

            DialogCommand = ICommand.From(OpenDialog);
        }
    }
}
