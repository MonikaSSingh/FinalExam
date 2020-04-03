using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityFramework;
using Pocos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ContextClass _context;

        public UserController(ContextClass context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPoco>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPoco>> GetUserPoco(int id)
        {
            var userPoco = await _context.User.FindAsync(id);

            if (userPoco == null)
            {
                return NotFound();
            }

            return userPoco;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPoco(int id, UserPoco userPoco)
        {
            if (id != userPoco.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userPoco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPocoExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserPoco>> PostUserPoco(UserPoco userPoco)
        {
            _context.User.Add(userPoco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPoco", new { id = userPoco.UserId }, userPoco);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserPoco>> DeleteUserPoco(int id)
        {
            var userPoco = await _context.User.FindAsync(id);
            if (userPoco == null)
            {
                return NotFound();
            }

            _context.User.Remove(userPoco);
            await _context.SaveChangesAsync();

            return userPoco;
        }

        private bool UserPocoExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}
