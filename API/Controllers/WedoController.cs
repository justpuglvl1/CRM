using CRM.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.DAL;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("api/wedo")]
    public class WedoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public WedoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<Wedo>> Get()
        {
            var notes = await _db.Wedo.ToListAsync();

            return Ok(notes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Wedo>> Get(int id)
        {
            var note = await _db.Wedo.FirstOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<ActionResult<Wedo>> Post(Wedo note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Wedo.Add(note);
            await _db.SaveChangesAsync();

            return Ok(note);
        }

        [HttpPut]
        public async Task<ActionResult<Wedo>> Put(Wedo note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_db.Wedo.Any(x => x.Id == note.Id))
            {
                return NotFound();
            }

            _db.Update(note);
            await _db.SaveChangesAsync();

            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Wedo>> Delete(int id)
        {
            var note = await _db.Wedo.FirstOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            _db.Wedo.Remove(note);
            await _db.SaveChangesAsync();

            return Ok(note);
        }


    }
}
