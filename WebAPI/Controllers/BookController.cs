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
    public class BookController : ControllerBase
    {
        private readonly ContextClass _context;

        public BookController(ContextClass context)
        {
            _context = context;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookPoco>>> GetBook()
        {
            return await _context.Book.ToListAsync();
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookPoco>> GetBookPoco(int id)
        {
            var bookPoco = await _context.Book.FindAsync(id);

            if (bookPoco == null)
            {
                return NotFound();
            }

            return bookPoco;
        }

        // PUT: api/Book/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookPoco(int id, BookPoco bookPoco)
        {
            if (id != bookPoco.BookId)
            {
                return BadRequest();
            }

            _context.Entry(bookPoco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookPocoExists(id))
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

        // POST: api/Book
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BookPoco>> PostBookPoco(BookPoco bookPoco)
        {
            _context.Book.Add(bookPoco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookPoco", new { id = bookPoco.BookId }, bookPoco);
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookPoco>> DeleteBookPoco(int id)
        {
            var bookPoco = await _context.Book.FindAsync(id);
            if (bookPoco == null)
            {
                return NotFound();
            }

            _context.Book.Remove(bookPoco);
            await _context.SaveChangesAsync();

            return bookPoco;
        }

        private bool BookPocoExists(int id)
        {
            return _context.Book.Any(e => e.BookId == id);
        }
    }
}
