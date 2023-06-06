using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CRM.Data;
using Test.Data;
using Microsoft.AspNetCore.Authorization;
using Test.DAL;
using CRM.Models;

namespace CRM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /*IDiary diaryDataApi = new DiaryApiStore();*/
        DiaryApiStore diary = new DiaryApiStore();
        ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var a =  await diaryDataApi.AllNotes();
            return View();
        }

        public IActionResult Notice() => View();

        [HttpGet]
        public async Task<IActionResult> CreateUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateUser(Notes model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            model.Iban = "Ожидание";
            model.Date = DateTime.Now.ToString();
            /*context.Notes.Add(model);
            await context.SaveChangesAsync();*/
            await diary.AddNoteAsync(model);
            return RedirectToAction("notice", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Service()
        {
            var a = await diary.AllAbout();
            return View(a);
        }

        [HttpGet]
        public async Task<IActionResult> Project()
        {
            var a = await diary.AllWedo();
            return View(a);
        }

        [HttpGet]
        public async Task<IActionResult> Blog()
        {
            var a = await diary.AllBlog();
            return View(a);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}