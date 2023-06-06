using CRM.Models;
using Newtonsoft.Json;
using System.Text;

namespace CRM.Data
{
    public class WedoDiary
    {
        private readonly HttpClient _httpClient;
        private const string _apiUrlWedo = @"https://localhost:1234/api/wedo";

        public WedoDiary()
        {
            _httpClient = new HttpClient();
        }

        public async Task AddWedoAsync(Wedo note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(_apiUrlWedo, content);
        }

        public async Task DeleteWedoAsync(int id)
        {
            await _httpClient.DeleteAsync(_apiUrlWedo + $"/{id}");
        }

        public async Task<Wedo> GetWedoByIdAsync(int id)
        {
            string json = await _httpClient.GetStringAsync(_apiUrlWedo + $"/{id}");
            return JsonConvert.DeserializeObject<Wedo>(json);
        }

        public async Task UpdateWedoAsync(Wedo note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(_apiUrlWedo, content);
        }
    }
}
