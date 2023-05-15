using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HueFesAPI;
using HueFesAPI.Data;

namespace HueFesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SupportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Supports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Support>>> GetSupport()
        {
          if (_context.Support == null)
          {
              return NotFound();
          }
            return await _context.Support.ToListAsync();
        }

        // GET: api/Supports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Support>> GetSupport(int id)
        {
          if (_context.Support == null)
          {
              return NotFound();
          }
            var support = await _context.Support.FindAsync(id);

            if (support == null)
            {
                return NotFound();
            }

            return support;
        }

        // PUT: api/Supports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupport(int id, Support support)
        {
            if (id != support.Id)
            {
                return BadRequest();
            }

            _context.Entry(support).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupportExists(id))
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

        // POST: api/Supports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Support>> PostSupport(Support support)
        {
          if (_context.Support == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Support'  is null.");
          }
            _context.Support.Add(support);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupport", new { id = support.Id }, support);
        }

        // DELETE: api/Supports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupport(int id)
        {
            if (_context.Support == null)
            {
                return NotFound();
            }
            var support = await _context.Support.FindAsync(id);
            if (support == null)
            {
                return NotFound();
            }

            _context.Support.Remove(support);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupportExists(int id)
        {
            return (_context.Support?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
