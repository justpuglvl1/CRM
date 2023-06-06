using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models;
using Infrastructure.Extensions;

namespace Data
{
    public class DiaryDataApi : IDiaryData
    {
        private readonly HttpClient _httpClient;
        private const string _apiUrl = @"https://localhost:1234/api/diary";

        public DiaryDataApi()
        {
            _httpClient = new HttpClient();
        }

        public IEnumerable<Notes> AllNotes()
        {
            string json = _httpClient.GetStringAsync(_apiUrl).Result;
            var notes = JsonConvert.DeserializeObject<IEnumerable<Notes>>(json).ToObservableCollection();
            return notes;
        }

        public Notes GetNoteById(int id)
        {
            string json = _httpClient.GetStringAsync(_apiUrl + $"/{id}").Result;
            return JsonConvert.DeserializeObject<Notes>(json);
        }

        public void AddNote(Notes note)
        {
            note.Id = 0;
            note.Date = DateTime.Now.ToString();
            note.Iban = "Ожидается";
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = _httpClient.PostAsync(_apiUrl, content).Result;
        }

        public void DeleteNote(int id)
        {
            var result = _httpClient.DeleteAsync(_apiUrl + $"/{id}").Result;
        }

        public void UpdateNote(Notes note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = _httpClient.PutAsync(_apiUrl, content).Result;
        }
    }
}
