using System;
using System.Linq;
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
    [Authorize(Roles = "Admin")]

    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
          if (_context.Role == null)
          {
              return NotFound("Role is null");
          }
            return await _context.Role.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("name")]
        public async Task<ActionResult<Role>> GetRoleByName(string name)
        {
            if (_context.Role == null)
            {
                return NotFound();
            }
            var role = await _context.Role.FirstOrDefaultAsync(r => r.RoleName == name);

            if (role == null)
            {
                return NotFound("Role not found");
            }

            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(Guid id, Role role)
        {
            if (id != role.RoleId)
            {
                return BadRequest("Role not found");
            }

            _context.Entry(role).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Role updated!");
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
          if (_context.Role == null)
          {
              return Ok("Role is null");
          }
            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.RoleId }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (_context.Role == null)
            {
                return NotFound();
            }
            var role = await _context.Role.FindAsync(id);
            if (role == null)
            {
                return NotFound("Role not found");
            }

            _context.Role.Remove(role);
            await _context.SaveChangesAsync();

            return Ok("Role deleted!");
        }

        private bool RoleExists(Guid id)
        {
            return (_context.Role?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
