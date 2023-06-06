using BOT.Models;
using CRM.API.Models;
using Newtonsoft.Json;
using System.Text;


namespace BOT.Data
{
    public class DiaryApiStore
    {
        private readonly HttpClient _httpClient;
        private const string _apiUrlWedo = @"https://localhost:1234/api/wedo";
        private const string _apiUrlBlog = @"https://localhost:1234/api/blog";
        private const string _apiUrlAbout = @"https://localhost:1234/api/about";
        private const string _apiUrl = @"https://localhost:1234/api/diary";

        public DiaryApiStore()
        {
            _httpClient = new HttpClient();
        }

        public async Task AddNoteAsync(Notes note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(_apiUrl, content);
        }

        public async Task<IEnumerable<Wedo>> AllWedo()
        {
            string json = await _httpClient.GetStringAsync(_apiUrlWedo);
            return JsonConvert.DeserializeObject<IEnumerable<Wedo>>(json);
        }

        public async Task<IEnumerable<Blog>> AllBlog()
        {
            string json = await _httpClient.GetStringAsync(_apiUrlBlog);
            return JsonConvert.DeserializeObject<IEnumerable<Blog>>(json);
        }

        public async Task<IEnumerable<About>> AllAbout()
        {
            string json = await _httpClient.GetStringAsync(_apiUrlAbout);
            return JsonConvert.DeserializeObject<IEnumerable<About>>(json);
        }
    }
}
