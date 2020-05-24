using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TypeOneFoodJournal.Entities;
using TypeOneFoodJournal.Data;
using TypeOneFoodJournal.Models;

namespace TypeOneFoodJournal.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalEntryController : ControllerBase
    {
        private readonly FoodJournalContext context;

        public JournalEntryController() : base()
        {
            this.context = null;
        }

        //[Authorize]
        [HttpGet]
        [Route("[controller]/SearchJournal")]
        public IActionResult SearchJournal(string searchValue)
        {
            var upperSearchValue = searchValue.ToUpper();
            var retVal = new List<JournalEntryModel>();
            try
            {
                {
                    var results = this.context.JournalEntries.Where(entry => entry.Title.ToUpper().Contains(upperSearchValue));

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
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(retVal.Take(100));
        }

        //[Authorize]
        [HttpPost]
        [Route("api/journalEntry/SaveJournalEntry")]
        public IActionResult SaveJournalEntry([FromBody] JournalEntryModel entry)
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
                        var entryFromDb = this.context.JournalEntries.FirstOrDefault(x => x.Id == entry.Id);

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

        //[Authorize]
        [HttpPut]
        [Route("api/journalEntry/RemoveJournalEntryTag")]
        public IActionResult RemoveJournalEntryTag(int journalEntryTagId)
        {
            {
                var journalEntryTagFromDb = this.context.JournalEntryTags.FirstOrDefault(x => x.Id == journalEntryTagId);

                if (journalEntryTagFromDb != null)
                {
                    this.context.JournalEntryTags.Remove(journalEntryTagFromDb);
                    this.context.SaveChanges();
                }
            }

            return Ok();
        }

        //[Authorize]
        [HttpGet]
        [Route("api/journalEntry/SearchTags")]
        public IActionResult SearchTags(string searchValue)
        {
            var upperSearchValue = searchValue.ToUpper();
            var retVal = new List<JournalEntryModel>();
            try
            {
                {
                    var results = this.context.Tags.Where(entry => entry.Description.ToUpper().Contains(upperSearchValue));

                    return Ok(results.Take(10).ToList());
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Authorize]
        [HttpPost]
        [Route("api/journalEntry/SaveTag")]
        public IActionResult SaveTag([FromBody] Tag tag)
        {
            try
            {
                if (tag == null)
                {
                    throw new ArgumentNullException(nameof(tag));
                }

                {
                    try
                    {
                        var tagFromDb = this.context.Tags.FirstOrDefault(x => x.Id == tag.Id);

                        if (tagFromDb == null)
                        {
                            tagFromDb = new Tag();
                        }

                        tagFromDb.Description = tag.Description;

                        if (tag.Id == 0)
                        {
                            this.context.Tags.Add(tagFromDb);
                        }

                        this.context.SaveChanges();
                        tag.Id = tagFromDb.Id;
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

                return Ok(tag.Id);
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
    }
}
