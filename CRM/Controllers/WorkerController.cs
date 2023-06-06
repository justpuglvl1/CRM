using CRM.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Http;
using Test.DAL;
using Microsoft.EntityFrameworkCore;
using Test.Domain.ViewModel;
using Microsoft.AspNetCore;
using System.Net.Http.Headers;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using CRM.Models;
using CRM.Repository;

namespace CRM.Controllers
{

    [Authorize(Roles = "Admin")]
    public class WorkerController : Controller
    {
        DiaryApiStore diary = new DiaryApiStore();
        AboutDiary aboutDiary = new AboutDiary();
        BlogDiary blogDiary = new BlogDiary();
        WedoDiary wedoDiary = new WedoDiary();
        WorkerAction WorkerAction = new WorkerAction();
        private readonly ApplicationDbContext _db;
        private IWebHostEnvironment _hostingEnvironment;

        public WorkerController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _hostingEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var app = await diary.AllNotesAsync();
            return View(app);
        }

        [HttpGet]
        public async Task<IActionResult> EditIndex(int id)
        {
            var nv = await WorkerAction.EditIndex(id);
            return View(nv);
        }

        [HttpPost]
        public async Task<IActionResult> EditIndex(Notes note)
        {
            await diary.UpdateNoteAsync(note);
            return RedirectToAction("Index", "Worker");
        }

        [HttpGet]
        public async Task<IActionResult> Blog()
        {
            var app = await diary.AllBlog();
            return View(app);
        }

        public IActionResult CreateBlog() => View();

        [HttpPost]
        public async Task<IActionResult> CreateBlog(List<IFormFile> files, Blog model)
        {
            await WorkerAction.CreateBlog(files, model);
            return RedirectToAction("blog", "Worker");

        }
        [HttpGet]
        public async Task<IActionResult> EditBlog(int id)
        {
            var nv = await WorkerAction.EditBlog(id);
            return View(nv);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(List<IFormFile> files, Blog note)
        {
            await WorkerAction.EditBlog(files, note);
            return RedirectToAction("Blog", "Worker");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            await blogDiary.DeleteBlogAsync(id);
            return RedirectToAction("blog", "Worker");
        }

        [HttpGet]
        public async Task<IActionResult> Wedo()
        {
            var app = await diary.AllWedo();
            return View(app);
        }

        public IActionResult CreateWedo() => View();

        [HttpPost]
        public async Task<IActionResult> CreateWedo(Wedo model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            await wedoDiary.AddWedoAsync(model);
            return RedirectToAction("Wedo", "Worker");
        }

        [HttpGet]
        public async Task<IActionResult> EditWedo(int id)
        {
            var nv = await WorkerAction.EditWedo(id);
            return View(nv);
        }

        [HttpPost]
        public async Task<IActionResult> EditWedo(Wedo note)
        {
            await wedoDiary.UpdateWedoAsync(note);
            return RedirectToAction("Wedo", "Worker");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWedo(int id)
        {
            await wedoDiary.DeleteWedoAsync(id);
            return RedirectToAction("Wedo", "Worker");
        }

        [HttpGet]
        public async Task<IActionResult> About()
        {
            var app = await diary.AllAbout();
            return View(app);
        }

        public IActionResult CreateAbout() => View();
        

        [HttpPost]
        public async Task<IActionResult> CreateAbout(List<IFormFile> files, About model)
        {
            await WorkerAction.CreateAbout(files, model);
            return RedirectToAction("about", "Worker");
        }

        [HttpGet]
        public async Task<IActionResult> EditAbout(int id)
        {
            var nv = await WorkerAction.EditAbout(id);
            return View(nv);
        }

        [HttpPost]
        public async Task<IActionResult> EditAbout(List<IFormFile> files, About note)
        {
            await WorkerAction.EditAbout(files, note);
            return RedirectToAction("About", "Worker");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            await aboutDiary.DeleteAboutAsync(id);
            return RedirectToAction("About", "Worker");
        }

        [HttpGet]
        public async Task<IActionResult> Today()
        {
            var notes = await WorkerAction.Today();
            return View(notes);
        }

        [HttpGet]
        public async Task<IActionResult> Yesterday()
        {
            var notes = await WorkerAction.Yesterday();
            return View(notes);
        }

        [HttpGet]
        public async Task<IActionResult> Week()
        {
            var notes = await WorkerAction.Week();
            return View(notes);
        }

        [HttpGet]
        public async Task<IActionResult> Month()
        {
            var notes = await WorkerAction.Month();
            return View(notes);
        }

        [HttpGet]
        public async Task<IActionResult> Range()
        {
            var form = Request.Form;
            string c = form["1"];
            string[] date1 = c.Split('-');
            if (date1[2].Contains("0"))
                date1[2] = int.Parse(date1[2]).ToString();
            c = date1[2] + "." + date1[1] + "." + date1[0];

            string ba = form["2"];
            string[] date2 = ba.Split('-');
            ba = date2[2] + "." + date2[1] + "." + date2[0];

            var notes = await WorkerAction.Range(c, ba);
            return View(notes);
        }
    }
}
