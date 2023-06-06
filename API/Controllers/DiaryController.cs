using CRM.API.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Test.DAL;
using Test.Models;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/diary")]
    public class DiaryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public DiaryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<Notes>> Get()
        {
            var notes = await _db.Notes.ToListAsync();

            return Ok(notes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Notes>> Get(int id)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<ActionResult<Notes>> Post(Notes note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Notes.Add(note);
            await _db.SaveChangesAsync();

            return Ok(note);
        }

        [HttpPut]
        public async Task<ActionResult<Notes>> Put(Notes note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_db.Notes.Any(x => x.Id == note.Id))
            {
                return NotFound();
            }

            _db.Update(note);
            await _db.SaveChangesAsync();

            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Notes>> Delete(int id)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();

            return Ok(note);
        }


    }
}
