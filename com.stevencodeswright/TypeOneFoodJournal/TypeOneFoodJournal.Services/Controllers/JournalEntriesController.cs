using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypeOneFoodJournal.Data;
using TypeOneFoodJournal.Entities;
using TypeOneFoodJournal.Models;

namespace TypeOneFoodJournal.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalEntriesController : ControllerBase
    {
        private readonly FoodJournalContext context;

        public JournalEntriesController(FoodJournalContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JournalEntryModel>>> GetJournalEntries([FromQuery] string searchValue)
        {
            var upperSearchValue = searchValue.ToUpper();
            var retVal = new List<JournalEntryModel>();
            try
            {
                var results = await this.context.JournalEntries
                                                .Where(entry => entry.Title.ToUpper().Contains(upperSearchValue) || 
                                                                entry.JournalEntryTags.Where(t=>t.Tag.Description.ToUpper().Contains(upperSearchValue)).Any())
                                                                .ToListAsync();

                if (results.Any())
                {
                    foreach (var result in results)
                    {

                        var currentEntry = new JournalEntryModel();
                        currentEntry.Tags = new List<TagModel>();
                        currentEntry.Id = result.Id;
                        currentEntry.Logged = result.Logged;
                        currentEntry.Notes = result.Notes;
                        currentEntry.Title = result.Title;

                        if (result.JournalEntryNutritionalInfos != null && result.JournalEntryNutritionalInfos.Any())
                        {
                            var nutrition = result.JournalEntryNutritionalInfos.FirstOrDefault().NutritionalInfo;
                            if (nutrition.Id > 0)
                            {
                                currentEntry.NutritionalInfo = new NutritionalInfoModel();
                                currentEntry.NutritionalInfo.Id = nutrition.Id;
                                currentEntry.NutritionalInfo.Calories = nutrition.Calories;
                                currentEntry.NutritionalInfo.Carbohydrates = nutrition.Carbohydrates;
                                currentEntry.NutritionalInfo.Protein = nutrition.Protein;
                            }
                        }

                        if (result.JournalEntryDoses != null && result.JournalEntryDoses.Any())
                        {
                            var dose = result.JournalEntryDoses.FirstOrDefault().Dose;
                            if (dose.Id > 0)
                            {
                                currentEntry.Dose = new DoseModel();
                                currentEntry.Dose.Id = dose.Id;
                                currentEntry.Dose.Extended = dose.Extended;
                                currentEntry.Dose.InsulinAmount = dose.InsulinAmount;
                                currentEntry.Dose.TimeExtended = dose.TimeExtended;
                                currentEntry.Dose.TimeOffset = dose.TimeOffset;
                                currentEntry.Dose.UpFront = dose.UpFront;
                            }
                        }


                        if (result.JournalEntryTags != null && result.JournalEntryTags.Any())
                        {
                            foreach (var tag in result.JournalEntryTags)
                            {
                                var tagViewModel = new TagModel();
                                tagViewModel.Id = tag.Tag.Id;
                                tagViewModel.Description = tag.Tag.Description;
                                currentEntry.Tags.Add(tagViewModel);
                            }
                        }

                        retVal.Add(currentEntry);
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(retVal.Take(100));
        }

        // GET: api/JournalEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JournalEntry>> GetJournalEntry(int id)
        {
            var journalEntry = await context.JournalEntries.FindAsync(id);

            if (journalEntry == null)
            {
                return NotFound();
            }

            return journalEntry;
        }

        // PUT: api/JournalEntries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJournalEntry(int id, JournalEntry journalEntry)
        {
            if (id != journalEntry.Id)
            {
                return BadRequest();
            }

            context.Entry(journalEntry).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JournalEntryExists(id))
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

        // POST: api/JournalEntries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<JournalEntry>> PostJournalEntry(JournalEntry journalEntry)
        {
            context.JournalEntries.Add(journalEntry);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetJournalEntry", new { id = journalEntry.Id }, journalEntry);
        }

        // DELETE: api/JournalEntries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JournalEntry>> DeleteJournalEntry(int id)
        {
            var journalEntry = await context.JournalEntries.FindAsync(id);
            if (journalEntry == null)
            {
                return NotFound();
            }

            context.JournalEntries.Remove(journalEntry);
            await context.SaveChangesAsync();

            return journalEntry;
        }

        private bool JournalEntryExists(int id)
        {
            return context.JournalEntries.Any(e => e.Id == id);
        }
    }
}
