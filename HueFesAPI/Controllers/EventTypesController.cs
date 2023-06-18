using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HueFesAPI;
using HueFesAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace HueFesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventType>>> GetEventType()
        {
          if (_context.EventType == null)
          {
              return NotFound();
          }
            return await _context.EventType.ToListAsync();
        }

        // GET: api/EventTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventType>> GetEventType(int id)
        {
          if (_context.EventType == null)
          {
              return NotFound();
          }
            var eventType = await _context.EventType.FindAsync(id);

            if (eventType == null)
            {
                return NotFound();
            }

            return eventType;
        }

        // PUT: api/EventTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventType(int id, EventType eventType)
        {
            if (id != eventType.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventTypeExists(id))
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

        // POST: api/EventTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventType>> PostEventType(EventType eventType)
        {
          if (_context.EventType == null)
          {
              return Problem("Entity set 'ApplicationDbContext.EventType'  is null.");
          }
            _context.EventType.Add(eventType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventType", new { id = eventType.Id }, eventType);
        }

        // DELETE: api/EventTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventType(int id)
        {
            if (_context.EventType == null)
            {
                return NotFound();
            }
            var eventType = await _context.EventType.FindAsync(id);
            if (eventType == null)
            {
                return NotFound();
            }

            _context.EventType.Remove(eventType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventTypeExists(int id)
        {
            return (_context.EventType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
