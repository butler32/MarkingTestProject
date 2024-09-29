using MarkingTestProject.Core.DTOS;
using MarkingTestProject.Interfaces;
using MarkingTestProject.Models;
using MarkingTestProject.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows;

namespace MarkingTestProject.ViewModels
{
    public class PalletViewModel : BaseViewModel
    {
        private IDataBaseService _dataBaseService;
        private CurrentTaskModel _currentTask;

        private ObservableCollection<PalletDTO> pallets;
        public ObservableCollection<PalletDTO> Pallets
        {
            get => pallets;
            set { pallets = value; OnPropertyChanged(); }
        }

        public ICommand GetDataCommand { get; }
        public ICommand ExportCommand { get; }

        private async Task CreateJsonFile()
        {
            try
            {
                var pallets = Pallets.Select(x => new
                {
                    x.Id,
                    x.Code,
                    Boxes = x.Boxes.Select(y => new
                    {
                        y.Id,
                        y.Code,
                        Bottles = y.Bottles.Select(b => new
                        {
                            b.Id,
                            b.Code
                        })
                    })
                })
                .ToList();

                var result = new
                {
                    ProductName = _currentTask.Name,
                    Gtin = _currentTask.Gtin,
                    BoxFormat = _currentTask.BoxFormat,
                    PalletFormat = _currentTask.PalletFormat,
                    Pallets = pallets
                };

                string json = JsonConvert.SerializeObject(result, Formatting.Indented);

                string fileName = $"{_currentTask.Gtin}_result_file_{DateTime.Now.ToString("ddMMyy_HHmm")}.json";

                File.WriteAllText(fileName, json);

                MessageBox.Show("JSON file exported successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async Task GetData()
        {
            try
            {
                var data = await _dataBaseService.GetAllData(_currentTask.Gtin);

                Pallets = new ObservableCollection<PalletDTO>(data.ToList().OrderBy(x => x.Id));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public PalletViewModel(IDataBaseService dataBaseService, CurrentTaskModel currentTask)
        {
            _currentTask = currentTask;
            _dataBaseService = dataBaseService;

            GetDataCommand = ICommand.From(GetData);
            ExportCommand = ICommand.From(CreateJsonFile);
            GetDataCommand.Execute(null);
        }
    }
}
