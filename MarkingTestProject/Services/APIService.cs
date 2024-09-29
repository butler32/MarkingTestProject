using MarkingTestProject.Interfaces;
using MarkingTestProject.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Windows;

namespace MarkingTestProject.Services
{
    public class APIService : IAPIService
    {
        private string _url = "http://promark94.marking.by/client/api/get/task/";
        HttpClient _httpClient = new HttpClient();

        public CurrentTaskModel GetData()
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(_url).Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;

                JObject json = JObject.Parse(responseBody);

                return new CurrentTaskModel
                {
                    Volume = (string)json["mission"]["lot"]["package"]["volume"],
                    BoxFormat = (int)json["mission"]["lot"]["package"]["boxFormat"],
                    PalletFormat = (int)json["mission"]["lot"]["package"]["palletFormat"],
                    Name = (string)json["mission"]["lot"]["product"]["name"],
                    Gtin = (string)json["mission"]["lot"]["product"]["gtin"]
                };
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
