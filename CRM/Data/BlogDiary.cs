using Newtonsoft.Json;
using System.Text;
using CRM.Models;

namespace CRM.Data
{
    public class BlogDiary
    {
        private readonly HttpClient _httpClient;
        private const string _apiUrlBlog = @"https://localhost:1234/api/blog";

        public BlogDiary()
        {
            _httpClient = new HttpClient();
        }

        public async Task AddBlogAsync(Blog note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(_apiUrlBlog, content);
        }

        public async Task DeleteBlogAsync(int id)
        {
            await _httpClient.DeleteAsync(_apiUrlBlog + $"/{id}");
        }

        public async Task<Blog> GetAboutByIdAsync(int id)
        {
            string json = await _httpClient.GetStringAsync(_apiUrlBlog + $"/{id}");
            return JsonConvert.DeserializeObject<Blog>(json);
        }

        public async Task UpdateAboutAsync(Blog note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(_apiUrlBlog, content);
        }
    }
}
