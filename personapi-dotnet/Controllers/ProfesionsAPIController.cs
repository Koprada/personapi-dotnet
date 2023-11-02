using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionsAPIController : ControllerBase
    {
        private readonly dbContext _context;

        public ProfesionsAPIController(dbContext context)
        {
            _context = context;
        }

        // GET: api/ProfesionsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesion>>> GetProfesions()
        {
          if (_context.Profesions == null)
          {
              return NotFound();
          }
            return await _context.Profesions.ToListAsync();
        }

        // GET: api/ProfesionsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesion>> GetProfesion(int id)
        {
          if (_context.Profesions == null)
          {
              return NotFound();
          }
            var profesion = await _context.Profesions.FindAsync(id);

            if (profesion == null)
            {
                return NotFound();
            }

            return profesion;
        }

        // PUT: api/ProfesionsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesion(int id, Profesion profesion)
        {
            if (id != profesion.Id)
            {
                return BadRequest();
            }

            _context.Entry(profesion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProfesionsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profesion>> PostProfesion(Profesion profesion)
        {
          if (_context.Profesions == null)
          {
              return Problem("Entity set 'dbContext.Profesions'  is null.");
          }
            _context.Profesions.Add(profesion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfesionExists(profesion.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProfesion", new { id = profesion.Id }, profesion);
        }

        // DELETE: api/ProfesionsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesion(int id)
        {
            if (_context.Profesions == null)
            {
                return NotFound();
            }
            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }

            _context.Profesions.Remove(profesion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesionExists(int id)
        {
            return (_context.Profesions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
