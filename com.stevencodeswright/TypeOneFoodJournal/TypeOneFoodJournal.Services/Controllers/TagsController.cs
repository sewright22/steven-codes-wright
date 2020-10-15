using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypeOneFoodJournal.Business;
using TypeOneFoodJournal.Data;
using TypeOneFoodJournal.Entities;

namespace TypeOneFoodJournal.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly FoodJournalContext _context;

        public TagsController(FoodJournalContext context)
        {
            _context = context;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags(string searchValue, int? journalEntryId = null)
        {

            if (string.IsNullOrEmpty(searchValue) && journalEntryId.HasValue == false)
            {
                var results = await _context.Tags.Distinct().ToListAsync();

                return Ok(results);
            }
            else if (journalEntryId.HasValue)
            {
                var journalEntry = await _context.JournalEntries.FirstOrDefaultAsync(je => je.Id==journalEntryId.Value);

                var tags = journalEntry?.GetTags();

                if(string.IsNullOrEmpty(searchValue)==false)
                {
                    tags = tags.Where(t=>t.Description.ToUpper().Contains(searchValue.ToUpper()));
                }

                return Ok(tags);
            }
            else
            {
                var upperSearchValue = searchValue.ToUpper();
                var results = await _context.Tags.Where(entry => entry.Description.ToUpper().Contains(upperSearchValue)).ToListAsync();

                return Ok(results);
            }
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        // PUT: api/Tags/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, Tag tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }

            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tags
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            var tagFromDb = _context.Tags.FirstOrDefault(x => x.Id == tag.Id);

            if (tagFromDb == null)
            {
                tagFromDb = new Tag();
            }

            tagFromDb.Description = tag.Description;

            if (tag.Id == 0)
            {
                _context.Tags.Add(tagFromDb);
            }

            await _context.SaveChangesAsync();
            tag.Id = tagFromDb.Id;

            return CreatedAtAction("GetTag", new { id = tag.Id }, tag);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tag>> DeleteTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return tag;
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}
