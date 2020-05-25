using DiabetesFoodJournal.Data.Server;
using DiabetesFoodJournal.Entities._4_5_2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class JournalEntryController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/journalEntry/SearchJournal")]
        public IHttpActionResult SearchJournal(string searchValue)
        {
            var upperSearchValue = searchValue.ToUpper();
            var retVal = new List<JournalEntryModel>();
            try
            {
                using (var db = new DiabetesFoodJournalContext())
                {
                    var results = db.JournalEntries.AsNoTracking().Where(entry => entry.Title.ToUpper().Contains(upperSearchValue))
                                                                                                       .Include(x => x.JournalEntryNutritionalInfos.Select(y => y.NutritionalInfo))
                                                                                                       .Include(x => x.JournalEntryDoses.Select(y => y.Dose))
                                                                                                       .Include(x => x.JournalEntryTags.Select(y => y.Tag));

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

                            if (result.JournalEntryNutritionalInfos!= null && result.JournalEntryNutritionalInfos.Any())
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
                            entry.Id = entryFromDb.Id;
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
                            entry.Dose.Id = doseFromDb.Id;
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
                            entry.NutritionalInfo.Id = nutritionalInfoFromDb.Id;
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

        [Authorize]
        [HttpGet]
        [Route("api/journalEntry/SearchTags")]
        public IHttpActionResult SearchTags(string searchValue)
        {
            var upperSearchValue = searchValue.ToUpper();
            var retVal = new List<JournalEntryModel>();
            try
            {
                using (var db = new DiabetesFoodJournalContext())
                {
                    var results = db.Tags.AsNoTracking().Where(entry => entry.Description.ToUpper().Contains(upperSearchValue));

                    return Ok(results.Take(10).ToList());
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/journalEntry/SaveTag")]
        public IHttpActionResult SaveTag([FromBody] Tag tag)
        {
            try
            {
                if (tag == null)
                {
                    throw new ArgumentNullException(nameof(tag));
                }

                using (var db = new DiabetesFoodJournalContext())
                {
                    try
                    {
                        var tagFromDb = db.Tags.FirstOrDefault(x => x.Id == tag.Id);

                        if (tagFromDb == null)
                        {
                            tagFromDb = db.Tags.Create();
                        }

                        tagFromDb.Description = tag.Description;

                        if (tag.Id == 0)
                        {
                            db.Tags.Add(tagFromDb);
                        }

                        db.SaveChanges();
                        tag.Id = tagFromDb.Id;
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