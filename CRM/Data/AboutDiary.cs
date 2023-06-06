using Newtonsoft.Json;
using System.Text;
using CRM.Models;

namespace CRM.Data
{
    public class AboutDiary
    {
        private readonly HttpClient _httpClient;
        private const string _apiUrlAbout = @"https://localhost:1234/api/about";

        public AboutDiary()
        {
            _httpClient = new HttpClient();
        }

        public async Task AddAboutAsync(About note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(_apiUrlAbout, content);
        }

        public async Task DeleteAboutAsync(int id)
        {
            await _httpClient.DeleteAsync(_apiUrlAbout + $"/{id}");
        }

        public async Task<About> GetAboutByIdAsync(int id)
        {
            string json = await _httpClient.GetStringAsync(_apiUrlAbout + $"/{id}");
            return JsonConvert.DeserializeObject<About>(json);
        }

        public async Task UpdateAboutAsync(About note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(_apiUrlAbout, content);
        }
    }
}
