using MarkingTestProject.Core.DTOS;
using MarkingTestProject.Core.Interfaces;
using MarkingTestProject.Interfaces;
using MarkingTestProject.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace MarkingTestProject.Services
{
    public class DataBaseService : IDataBaseService
    {
        private List<string> data;
        private List<ParsedLineModel> parsedLines;
        private IRepositoryService _repositoryService;
        private CurrentTaskModel _currentTask;
        private ICancellationTokenService _cancellationTokenService;

        public async Task<bool> ReadFile(string filePath, string targetGTIN, CurrentTaskModel currentTaskModel)
        {
            int bufferSize = 1024;

            List<string> lines = new List<string>();

            using(var fileStream = File.Open(filePath, FileMode.Open))
            {
                using (BufferedStream stream = new BufferedStream(fileStream, bufferSize))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line;
                        while ((line = (await reader.ReadLineAsync(_cancellationTokenService.GetToken()))!) is not null)
                        {
                            lines.Add(line);

                            if (lines.Count == bufferSize)
                            {
                                ProcessLines(lines, targetGTIN);
                                lines.Clear();
                            }
                        }

                        if (lines.Count > 0)
                        {
                            ProcessLines(lines, targetGTIN);
                        }

                    }
                }
            }
            

            _currentTask = currentTaskModel;

            Parse();
            SortProducts();

            return true;
        }

        private void ProcessLines(List<string> lines, string targetGTIN)
        {
            foreach (string line in lines)
            {
                if (IsValidCode(line) && line.Substring(2, 14) == targetGTIN)
                {
                    data.Add(line);
                }
            }
        }

        private bool IsValidCode(string code)
        {
            if (code.Length < 18)
            {
                return false;
            }

            string first18Chars = code.Substring(0, 18);
            return Regex.IsMatch(first18Chars, "^[0-9]{18}$");
        }

        private void Parse()
        {
            if (data.Count > 0)
            {
                foreach (string line in data)
                {
                    parsedLines.Add(new ParsedLineModel
                    {
                        FirstGroupCode = line.Substring(0, 2),
                        GTIN = line.Substring(2, 14),
                        SecondGroupCode = line.Substring(16, 2),
                        Info = line.Substring(18),
                    });
                }
            }
        }

        private void SortProducts()
        {
            List<PalletDTO> pallets = new List<PalletDTO>();
            List<BoxDTO> boxes = new List<BoxDTO>();
            List<BottleDTO> bottles = new List<BottleDTO>();

            foreach (var product in parsedLines)
            {
                for(int i = 0; i < _currentTask.PalletFormat; i++)
                {
                    for (int j = 0; j < _currentTask.BoxFormat; j++)
                    {
                        bottles.Add(new BottleDTO
                        {
                            Code = String.Concat(product.FirstGroupCode, product.GTIN, product.SecondGroupCode, product.Info),
                        });
                    }

                    boxes.Add(new BoxDTO
                    {
                        Bottles = new List<BottleDTO>(bottles),
                        Code = String.Concat(product.FirstGroupCode, product.GTIN, "37", bottles.Count, product.SecondGroupCode, boxes.Count + 1)
                    });

                    bottles.Clear();
                }

                pallets.Add(new PalletDTO
                {
                    Code = String.Concat(product.FirstGroupCode, product.GTIN, "37", boxes.Count, product.SecondGroupCode, pallets.Count + 1),
                    Boxes = new List<BoxDTO>(boxes)
                });

                boxes.Clear();
            }

            _repositoryService.DeleteAll(_cancellationTokenService.GetToken()).ContinueWith(x =>
            {
                try
                {
                    _repositoryService.Import(pallets, _cancellationTokenService.GetToken());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            });
        }

        public async Task<IEnumerable<PalletDTO>> GetAllPalletsData(string gtin)
        {
            return await _repositoryService.GetAllPalletsByGtin(gtin, _cancellationTokenService.GetToken());
        }

        public async Task<IEnumerable<BottleDTO>> GetAllBottlesData(string gtin)
        {
            return await _repositoryService.GetAllBottlesByGtin(gtin, _cancellationTokenService.GetToken());
        }

        public async Task<IEnumerable<BoxDTO>> GetAllBoxesData(string gtin)
        {
            return await _repositoryService.GetAllBoxesByGtin(gtin, _cancellationTokenService.GetToken());
        }

        public async Task<IEnumerable<PalletDTO>> GetAllData(string gtin)
        {
            return await _repositoryService.GetAllDataByGtinAsync(gtin, _cancellationTokenService.GetToken());
        }

        public DataBaseService(IRepositoryService repositoryService, ICancellationTokenService cancellationTokenService)
        {
            data = new List<string>();
            parsedLines = new List<ParsedLineModel>();

            _repositoryService = repositoryService;
            _cancellationTokenService = cancellationTokenService;
        }
    }
}
