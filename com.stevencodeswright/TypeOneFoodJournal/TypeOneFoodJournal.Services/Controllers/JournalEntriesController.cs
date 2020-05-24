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
        public async Task<ActionResult<JournalEntry>> PostJournalEntry([FromBody] JournalEntryModel entry)
        {

            try
            {
                if (entry == null)
                {
                    throw new ArgumentNullException(nameof(entry));
                }

                {
                    try
                    {
                        this.context.Database.BeginTransaction();
                        var entryFromDb = await this.context.JournalEntries.FirstOrDefaultAsync(x => x.Id == entry.Id);

                        if (entryFromDb == null)
                        {
                            entryFromDb = new JournalEntry();
                        }

                        entryFromDb.Logged = entry.Logged;
                        entryFromDb.Notes = entry.Notes ?? "";
                        entryFromDb.Title = entry.Title;

                        if (entryFromDb.Id == 0)
                        {
                            this.context.JournalEntries.Add(entryFromDb);
                            this.context.SaveChanges();
                            entry.Id = entryFromDb.Id;
                        }

                        var doseFromDb = this.context.Doses.FirstOrDefault(x => x.Id == entry.Dose.Id);

                        if (doseFromDb == null)
                        {
                            doseFromDb = new Dose();
                        }

                        doseFromDb.Extended = entry.Dose.Extended;
                        doseFromDb.InsulinAmount = entry.Dose.InsulinAmount;
                        doseFromDb.TimeExtended = entry.Dose.TimeExtended;
                        doseFromDb.TimeOffset = entry.Dose.TimeOffset;
                        doseFromDb.UpFront = entry.Dose.UpFront;

                        if (doseFromDb.Id == 0)
                        {
                            this.context.Doses.Add(doseFromDb);
                            this.context.SaveChanges();
                            entry.Dose.Id = doseFromDb.Id;
                        }

                        var entryDoseFromDb = this.context.JournalEntryDoses.FirstOrDefault(x => x.JournalEntryId == entryFromDb.Id && x.DoseId == doseFromDb.Id);

                        if (entryDoseFromDb == null)
                        {
                            entryDoseFromDb = new JournalEntryDose();

                            entryDoseFromDb.JournalEntryId = entryFromDb.Id;
                            entryDoseFromDb.DoseId = doseFromDb.Id;

                            this.context.JournalEntryDoses.Add(entryDoseFromDb);
                        }

                        var nutritionalInfoFromDb = this.context.NutritionalInfos.FirstOrDefault(x => x.Id == entry.NutritionalInfo.Id);

                        if (nutritionalInfoFromDb == null)
                        {
                            nutritionalInfoFromDb = new NutritionalInfo();
                        }

                        nutritionalInfoFromDb.Protein = entry.NutritionalInfo.Protein;
                        nutritionalInfoFromDb.Calories = entry.NutritionalInfo.Calories;
                        nutritionalInfoFromDb.Carbohydrates = entry.NutritionalInfo.Carbohydrates;

                        if (nutritionalInfoFromDb.Id == 0)
                        {
                            this.context.NutritionalInfos.Add(nutritionalInfoFromDb);
                            this.context.SaveChanges();
                            entry.NutritionalInfo.Id = nutritionalInfoFromDb.Id;
                        }

                        var entryNutritionalInfoFromDb = this.context.JournalEntryNutritionalInfos.FirstOrDefault(x => x.JournalEntryId == entryFromDb.Id && x.NutritionalInfoId == nutritionalInfoFromDb.Id);

                        if (entryNutritionalInfoFromDb == null)
                        {
                            entryNutritionalInfoFromDb = new JournalEntryNutritionalInfo();

                            entryNutritionalInfoFromDb.JournalEntryId = entryFromDb.Id;
                            entryNutritionalInfoFromDb.NutritionalInfoId = nutritionalInfoFromDb.Id;

                            this.context.JournalEntryNutritionalInfos.Add(entryNutritionalInfoFromDb);
                        }

                        foreach (var tag in entry.Tags)
                        {
                            var tagFromDb = this.context.Tags.FirstOrDefault(x => x.Id == tag.Id);

                            if (tagFromDb == null)
                            {
                                tagFromDb = new Tag();
                            }

                            tagFromDb.Description = tag.Description;

                            if (tagFromDb.Id == 0)
                            {
                                this.context.Tags.Add(tagFromDb);
                                this.context.SaveChanges();
                            }

                            var journalEntryTagFromDb = this.context.JournalEntryTags.FirstOrDefault(x => x.JournalEntryId == entryFromDb.Id && x.TagId == tagFromDb.Id);

                            if (journalEntryTagFromDb == null)
                            {
                                journalEntryTagFromDb = new JournalEntryTag();

                                journalEntryTagFromDb.JournalEntryId = entryFromDb.Id;
                                journalEntryTagFromDb.TagId = tagFromDb.Id;
                                this.context.JournalEntryTags.Add(journalEntryTagFromDb);
                            }
                        }

                        this.context.SaveChanges();
                        this.context.Database.CurrentTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        this.context.Database.CurrentTransaction.Rollback();
                        if (e.InnerException != null)
                        {
                            return BadRequest(e.InnerException.Message);
                        }
                        else
                        {
                            return BadRequest(e.Message);
                        }
                    }
                }

            //return CreatedAtAction("GetJournalEntry", new { id = journalEntry.Id }, journalEntry);
                return Ok(entry);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return BadRequest(e.InnerException.Message);
                }
                else
                {
                    return BadRequest(e.Message);
                }
            }
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
