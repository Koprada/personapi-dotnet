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
    public class TelefonoesAPIController : ControllerBase
    {
        private readonly dbContext _context;

        public TelefonoesAPIController(dbContext context)
        {
            _context = context;
        }

        // GET: api/TelefonoesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefonos()
        {
          if (_context.Telefonos == null)
          {
              return NotFound();
          }
            return await _context.Telefonos.ToListAsync();
        }

        // GET: api/TelefonoesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Telefono>> GetTelefono(string id)
        {
          if (_context.Telefonos == null)
          {
              return NotFound();
          }
            var telefono = await _context.Telefonos.FindAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            return telefono;
        }

        // PUT: api/TelefonoesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelefono(string id, Telefono telefono)
        {
            if (id != telefono.Num)
            {
                return BadRequest();
            }

            _context.Entry(telefono).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonoExists(id))
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

        // POST: api/TelefonoesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Telefono>> PostTelefono(Telefono telefono)
        {
          if (_context.Telefonos == null)
          {
              return Problem("Entity set 'dbContext.Telefonos'  is null.");
          }
            _context.Telefonos.Add(telefono);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TelefonoExists(telefono.Num))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTelefono", new { id = telefono.Num }, telefono);
        }

        // DELETE: api/TelefonoesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefono(string id)
        {
            if (_context.Telefonos == null)
            {
                return NotFound();
            }
            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            _context.Telefonos.Remove(telefono);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelefonoExists(string id)
        {
            return (_context.Telefonos?.Any(e => e.Num == id)).GetValueOrDefault();
        }
    }
}
