﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HueFesAPI;
using HueFesAPI.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HueFesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TicketTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TicketTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TicketTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketType>>> GetTicketType()
        {
          if (_context.TicketType == null)
          {
              return NotFound();
          }
            return await _context.TicketType.ToListAsync();
        }

        // GET: api/TicketTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketType>> GetTicketType(int id)
        {
          if (_context.TicketType == null)
          {
              return NotFound();
          }
            var ticketType = await _context.TicketType.FindAsync(id);

            if (ticketType == null)
            {
                return NotFound();
            }

            return ticketType;
        }

        // PUT: api/TicketTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketType(int id, TicketType ticketType)
        {
            if (id != ticketType.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticketType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketTypeExists(id))
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

        // POST: api/TicketTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketType>> PostTicketType(TicketType ticketType)
        {
          if (_context.TicketType == null)
          {
              return Problem("Entity set 'ApplicationDbContext.TicketType'  is null.");
          }
            _context.TicketType.Add(ticketType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketType", new { id = ticketType.Id }, ticketType);
        }

        // DELETE: api/TicketTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketType(int id)
        {
            if (_context.TicketType == null)
            {
                return NotFound();
            }
            var ticketType = await _context.TicketType.FindAsync(id);
            if (ticketType == null)
            {
                return NotFound();
            }

            _context.TicketType.Remove(ticketType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketTypeExists(int id)
        {
            return (_context.TicketType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
