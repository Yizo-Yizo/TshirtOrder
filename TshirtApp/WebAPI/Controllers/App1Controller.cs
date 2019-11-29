#define Primary
#if Primary
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

#region App1Controller
namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class App1Controller : ControllerBase
    {
        private readonly App1Context _context;
        #endregion

        public App1Controller(App1Context context)
        {
            _context = context;

            if (_context.App1Items.Count() == 0)
            {
                // Create a new App1Item if collection is empty,
                // which means you can't delete all App1Items.
                _context.App1Items.Add(new App1Item { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        #region snippet_GetAll
        // GET: api/App1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App1Item>>> GetApp1Items()
        {
            return await _context.App1Items.ToListAsync();
        }

        #region snippet_GetByID
        // GET: api/App1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App1Item>> GetApp1Item(long id)
        {
            var app1Item = await _context.App1Items.FindAsync(id);

            if (app1Item == null)
            {
                return NotFound();
            }

            return app1Item;
        }
        #endregion
        #endregion

        #region snippet_Create
        // POST: api/App1
        [HttpPost]
        public async Task<ActionResult<App1Item>> PostApp1Item(App1Item[] items)
        {
            foreach(var Item in items)
                {
                _context.App1Items.Add(Item);
                await _context.SaveChangesAsync();
                }
            return Ok();
        }
        #endregion

        #region snippet_Update
        // PUT: api/App1/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApp1Item(long id, App1Item item)
        {
            if (id != item.ID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/App1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var app1Item = await _context.App1Items.FindAsync(id);

            if (app1Item == null)
            {
                return NotFound();
            }

            _context.App1Items.Remove(app1Item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
#endif