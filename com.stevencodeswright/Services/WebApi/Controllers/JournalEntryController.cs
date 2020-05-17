using DiabetesFoodJournal.Data.Server;
using DiabetesFoodJournal.Entities._4_5_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class JournalEntryController : ApiController
    {
        //[Authorize]
        [HttpGet]
        [Route("api/journalEntry/SearchJournal")]
        public IHttpActionResult SearchJournal(string searchValue)
        {
            var retVal = new List<JournalEntryModel>();
            try
            {
                using (var db = new DiabetesFoodJournalContext())
                {
                    var results = from entry in db.JournalEntries.AsNoTracking()
                                  join entryTag in db.JournalEntryTags.AsNoTracking() on entry.Id equals entryTag.JournalEntryId
                                  join tag in db.Tags.AsNoTracking() on entryTag.TagId equals tag.Id
                                  join entryNutrition in db.JournalEntryNutritionalInfos.AsNoTracking() on entry.Id equals entryNutrition.JournalEntryId
                                  join nutrition in db.NutritionalInfos.AsNoTracking() on entryNutrition.NutritionalInfoId equals nutrition.Id
                                  join entryDose in db.JournalEntryDoses.AsNoTracking() on entry.Id equals entryDose.JournalEntryId
                                  join dose in db.Doses.AsNoTracking() on entryDose.DoseId equals dose.Id
                                  where entry.Title.ToUpper().Contains(searchValue.ToUpper()) || tag.Description.ToUpper().Contains(searchValue.ToUpper())
                                  select new
                                  {
                                      entry,
                                      tag,
                                      nutrition,
                                      dose
                                  };

                    var currentEntry = new JournalEntryModel();

                    if (results.Any())
                    {
                        foreach (var result in results)
                        {
                            if (result.entry.Id != currentEntry.Id)
                            {
                                if (currentEntry.Id > 0)
                                {
                                    retVal.Add(currentEntry);
                                }

                                currentEntry = new JournalEntryModel();
                                currentEntry.Tags = new List<TagModel>();
                                currentEntry.Id = result.entry.Id;
                                currentEntry.Logged = result.entry.Logged;
                                currentEntry.Notes = result.entry.Notes;
                                currentEntry.Title = result.entry.Title;

                                if (result.nutrition.Id > 0)
                                {
                                    currentEntry.NutritionalInfo = new NutritionalInfoModel();
                                    currentEntry.NutritionalInfo.Id = result.nutrition.Id;
                                    currentEntry.NutritionalInfo.Calories = result.nutrition.Calories;
                                    currentEntry.NutritionalInfo.Carbohydrates = result.nutrition.Carbohydrates;
                                    currentEntry.NutritionalInfo.Protein = result.nutrition.Protein;
                                }

                                if (result.dose.Id > 0)
                                {
                                    currentEntry.Dose = new DoseModel();
                                    currentEntry.Dose.Id = result.dose.Id;
                                    currentEntry.Dose.Extended = result.dose.Extended;
                                    currentEntry.Dose.InsulinAmount = result.dose.InsulinAmount;
                                    currentEntry.Dose.TimeExtended = result.dose.TimeExtended;
                                    currentEntry.Dose.TimeOffset = result.dose.TimeOffset;
                                    currentEntry.Dose.UpFront = result.dose.UpFront;
                                }
                            }

                            if (result.tag.Id > 0)
                            {
                                var tagViewModel = new TagModel();
                                tagViewModel.Id = result.tag.Id;
                                tagViewModel.Description = result.tag.Description;
                                currentEntry.Tags.Add(tagViewModel);
                            }
                        }
                    }

                    if (currentEntry.Id > 0)
                    {
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

        [Authorize]
        [HttpPost]
        [Route("api/journalEntry/SaveJournalEntry")]
        public IHttpActionResult SaveJournalEntry([FromBody] JournalEntryModel entry)
        {
            try
            {
                if (entry == null)
                {
                    throw new ArgumentNullException(nameof(entry));
                }

                using (var db = new DiabetesFoodJournalContext())
                {
                    try
                    {
                        db.Database.BeginTransaction();
                        var entryFromDb = db.JournalEntries.FirstOrDefault(x => x.Id == entry.Id);

                        if (entryFromDb == null)
                        {
                            entryFromDb = db.JournalEntries.Create();
                        }

                        entryFromDb.Logged = entry.Logged;
                        entryFromDb.Notes = entry.Notes ?? "";
                        entryFromDb.Title = entry.Title;

                        if (entryFromDb.Id == 0)
                        {
                            db.JournalEntries.Add(entryFromDb);
                            db.SaveChanges();
                        }

                        var doseFromDb = db.Doses.FirstOrDefault(x => x.Id == entry.Dose.Id);

                        if (doseFromDb == null)
                        {
                            doseFromDb = db.Doses.Create();
                        }

                        doseFromDb.Extended = entry.Dose.Extended;
                        doseFromDb.InsulinAmount = entry.Dose.InsulinAmount;
                        doseFromDb.TimeExtended = entry.Dose.TimeExtended;
                        doseFromDb.TimeOffset = entry.Dose.TimeOffset;
                        doseFromDb.UpFront = entry.Dose.UpFront;

                        if (doseFromDb.Id == 0)
                        {
                            db.Doses.Add(doseFromDb);
                            db.SaveChanges();
                        }

                        var entryDoseFromDb = db.JournalEntryDoses.FirstOrDefault(x => x.JournalEntryId == entryFromDb.Id && x.DoseId == doseFromDb.Id);

                        if (entryDoseFromDb == null)
                        {
                            entryDoseFromDb = db.JournalEntryDoses.Create();

                            entryDoseFromDb.JournalEntryId = entryFromDb.Id;
                            entryDoseFromDb.DoseId = doseFromDb.Id;

                            db.JournalEntryDoses.Add(entryDoseFromDb);
                        }

                        var nutritionalInfoFromDb = db.NutritionalInfos.FirstOrDefault(x => x.Id == entry.NutritionalInfo.Id);

                        if (nutritionalInfoFromDb == null)
                        {
                            nutritionalInfoFromDb = db.NutritionalInfos.Create();
                        }

                        nutritionalInfoFromDb.Protein = entry.NutritionalInfo.Protein;
                        nutritionalInfoFromDb.Calories = entry.NutritionalInfo.Calories;
                        nutritionalInfoFromDb.Carbohydrates = entry.NutritionalInfo.Carbohydrates;

                        if (nutritionalInfoFromDb.Id == 0)
                        {
                            db.NutritionalInfos.Add(nutritionalInfoFromDb);
                            db.SaveChanges();
                        }

                        var entryNutritionalInfoFromDb = db.JournalEntryNutritionalInfos.FirstOrDefault(x => x.JournalEntryId == entryFromDb.Id && x.NutritionalInfoId == nutritionalInfoFromDb.Id);

                        if (entryNutritionalInfoFromDb == null)
                        {
                            entryNutritionalInfoFromDb = db.JournalEntryNutritionalInfos.Create();

                            entryNutritionalInfoFromDb.JournalEntryId = entryFromDb.Id;
                            entryNutritionalInfoFromDb.NutritionalInfoId = nutritionalInfoFromDb.Id;

                            db.JournalEntryNutritionalInfos.Add(entryNutritionalInfoFromDb);
                        }

                        foreach (var tag in entry.Tags)
                        {
                            var tagFromDb = db.Tags.FirstOrDefault(x => x.Id == tag.Id);

                            if (tagFromDb == null)
                            {
                                tagFromDb = db.Tags.Create();
                            }

                            tagFromDb.Description = tag.Description;

                            if (tagFromDb.Id == 0)
                            {
                                db.Tags.Add(tagFromDb);
                                db.SaveChanges();
                            }

                            var journalEntryTagFromDb = db.JournalEntryTags.FirstOrDefault(x => x.JournalEntryId == entryFromDb.Id && x.TagId == tagFromDb.Id);

                            if (journalEntryTagFromDb == null)
                            {
                                journalEntryTagFromDb = db.JournalEntryTags.Create();

                                journalEntryTagFromDb.JournalEntryId = entryFromDb.Id;
                                journalEntryTagFromDb.TagId = tagFromDb.Id;
                                db.JournalEntryTags.Add(journalEntryTagFromDb);
                            }
                        }

                        db.SaveChanges();
                        db.Database.CurrentTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        db.Database.CurrentTransaction.Rollback();

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
                return Ok();
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

        [Authorize]
        [HttpPut]
        [Route("api/journalEntry/RemoveJournalEntryTag")]
        public IHttpActionResult RemoveJournalEntryTag(int journalEntryTagId)
        {
            using (var db = new DiabetesFoodJournalContext())
            {
                var journalEntryTagFromDb = db.JournalEntryTags.FirstOrDefault(x => x.Id == journalEntryTagId);

                if (journalEntryTagFromDb != null)
                {
                    db.JournalEntryTags.Remove(journalEntryTagFromDb);
                    db.SaveChanges();
                }
            }

            return Ok();
        }
    }
}