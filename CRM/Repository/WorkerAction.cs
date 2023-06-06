using CRM.Data;
using Microsoft.AspNetCore.Mvc;
using CRM.Models;
using Azure.Core;

namespace CRM.Repository
{
    public class WorkerAction
    {
        DiaryApiStore diary = new DiaryApiStore();
        AboutDiary aboutDiary = new AboutDiary();
        BlogDiary blogDiary = new BlogDiary();
        WedoDiary wedoDiary = new WedoDiary();
        public async Task<Notes> EditIndex(int id)
        {
            var note = await diary.GetNoteByIdAsync(id);

            Notes nv = new Notes()
            {
                Id = note.Id,
                Date = note.Date,
                Name = note.Name,
                Description = note.Description,
                Address = note.Address,
                Iban = note.Iban
            };
            return nv;
        }

        public async Task CreateBlog(List<IFormFile> files, Blog model)
        {
            var a = await diary.AllBlog();
            string filePath = "";
            foreach (var file in files)
            {
                filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory() + @"\wwwroot\image\blog\"), file.FileName);
                if (!System.IO.File.Exists(filePath))
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);
                model.Path = file.FileName;
            }

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] image = br.ReadBytes((int)fs.Length);
            model.Url = image;
            await blogDiary.AddBlogAsync(model);
        }

        public async Task<Blog> EditBlog(int id)
        {
            var note = await blogDiary.GetAboutByIdAsync(id);
            Blog nv = new Blog()
            {
                Id = note.Id,
                Name = note.Name,
                Url = note.Url,
                Text = note.Text
            };
            return nv;
        }

        public async Task EditBlog(List<IFormFile> files, Blog note)
        {
            string filePath = "";
            foreach (var file in files)
            {
                filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory() + @"\wwwroot\image\blog\"), file.FileName);
                System.IO.File.Delete(filePath);
                if (!System.IO.File.Exists(filePath))
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);
                note.Path = file.FileName;
            }
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] image = br.ReadBytes((int)fs.Length);
            note.Url = image;
            await blogDiary.UpdateAboutAsync(note);
        }

        public async Task<Wedo> EditWedo(int id)
        {
            var note = await wedoDiary.GetWedoByIdAsync(id);

            Wedo nv = new Wedo()
            {
                Id = note.Id,
                Name = note.Name,
                Text = note.Text
            };
            return nv;
        }

        public async Task<List<Notes>> Today()
        {
            var a = await diary.AllNotesAsync();
            List<Notes> notes = new List<Notes>();
            foreach (var note in a)
            {
                string date = DateTime.Now.ToShortDateString();
                if (note.Date.Contains(DateTime.Now.ToShortDateString()))
                    notes.Add(note);
            }
            return notes;
        }

        public async Task<List<Notes>> Yesterday()
        {
            var a = await diary.AllNotesAsync();
            List<Notes> notes = new List<Notes>();
            string date = DateTime.Now.ToShortDateString();
            string[] strings = date.Split('.');
            if (int.Parse(strings[0]) - 1 == 0)
            {
                strings[0] = "31";
                strings[1] = (int.Parse(strings[1]) - 1).ToString();
            }
            else
                strings[0] = (int.Parse(strings[0]) - 1).ToString();
            string yesterday = $"{strings[0]}.{strings[1]}.{strings[2]}";
            foreach (var note in a)
            {
                if (note.Date.Contains(yesterday))
                    notes.Add(note);
            }
            return notes;
        }

        public async Task<List<Notes>> Week()
        {
            var a = await diary.AllNotesAsync();
            List<Notes> notes = new List<Notes>();
            string date = DateTime.Now.ToShortDateString();
            string[] strings = date.Split('.');
            string day = strings[0];
            string mounth = strings[1];
            string year = strings[2];
            foreach (var note in a)
            {
                for (int i = 0; i < 7; i++)
                {
                    string yesterday = $"{strings[0]}.{strings[1]}.{strings[2]}";
                    if (note.Date.Contains(yesterday))
                        notes.Add(note);
                    if (int.Parse(strings[0]) - 1 == 0)
                    {
                        strings[0] = "31";
                        strings[1] = (int.Parse(strings[1]) - 1).ToString();
                        if (int.Parse(strings[1]) >= 1 || int.Parse(strings[1]) <= 9)
                            strings[1] = "0" + strings[1];
                    }
                    else
                        strings[0] = (int.Parse(strings[0]) - 1).ToString();
                }
                strings[0] = day; strings[1] = mounth; strings[2] = year;
            }
            return notes;
        }

        public async Task<List<Notes>> Month()
        {
            var a = await diary.AllNotesAsync();
            List<Notes> notes = new List<Notes>();
            string date = DateTime.Now.ToShortDateString();
            string[] strings = date.Split('.');
            string day = strings[0];
            string mounth = strings[1];
            string year = strings[2];
            foreach (var note in a)
            {
                for (int i = 0; i < 32; i++)
                {
                    string yesterday = $"{strings[0]}.{strings[1]}.{strings[2]}";
                    if (note.Date.Contains(yesterday))
                    {
                        notes.Add(note);
                        break;
                    }
                    if (int.Parse(strings[0]) - 1 == 0)
                    {
                        strings[0] = "31";
                        strings[1] = (int.Parse(strings[1]) - 1).ToString();
                        if (int.Parse(strings[1]) >= 1 || int.Parse(strings[1]) <= 9)
                            strings[1] = "0" + strings[1];
                    }
                    else
                        strings[0] = (int.Parse(strings[0]) - 1).ToString();
                }
                strings[0] = day; strings[1] = mounth; strings[2] = year;
            }
            return notes;
        }

        public async Task<List<Notes>> Range(string c, string ba)
        {
            var ab = await diary.AllNotesAsync();
            List<Notes> notes = new List<Notes>();
            string a = c;
            string b = ba;
            string[] date = b.Split('.');
            string day = date[0];
            string month = date[1];
            string year = date[2];
            foreach (var note in ab)
            {
                for (int i = 0; i < 32; i--)
                {
                    string yesterday = $"{date[0]}.{date[1]}.{date[2]}";

                    if (note.Date.Contains(yesterday))
                    {
                        notes.Add(note);
                        break;
                    }
                    if (int.Parse(date[0]) - 1 == 0)
                    {
                        date[0] = "31";
                        date[1] = (int.Parse(date[1]) - 1).ToString();
                        if (int.Parse(date[1]) >= 1 || int.Parse(date[1]) <= 9)
                            date[1] = "0" + date[1];
                    }
                    else
                        date[0] = (int.Parse(date[0]) - 1).ToString();
                    if (a == yesterday)
                        break;
                }
                date[0] = day; date[1] = month; date[2] = year;
            }
            return notes;
        }

        public async Task CreateAbout(List<IFormFile> files, About model)
        {
            var a = await diary.AllAbout();
            string filePath = "";
            foreach (var file in files)
            {
                filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory() + @"\wwwroot\image\about\"), file.FileName);
                if (!System.IO.File.Exists(filePath))
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);
                model.Path = file.FileName;
            }

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] image = br.ReadBytes((int)fs.Length);
            model.Url = image;
            await aboutDiary.AddAboutAsync(model);
        }

        public async Task<About> EditAbout(int id)
        {
            var note = await aboutDiary.GetAboutByIdAsync(id);


            About nv = new About()
            {
                Id = note.Id,
                Name = note.Name,
                Url = note.Url,
                Text = note.Text
            };
            return nv;
        }

        public async Task EditAbout(List<IFormFile> files, About note)
        {
            string filePath = "";
            foreach (var file in files)
            {
                filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory() + @"\wwwroot\image\about\"), file.FileName);
                if (!System.IO.File.Exists(filePath))
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);
                note.Path = file.FileName;
            }

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] image = br.ReadBytes((int)fs.Length);
            note.Url = image;
            await aboutDiary.UpdateAboutAsync(note);
        }
    }
}
