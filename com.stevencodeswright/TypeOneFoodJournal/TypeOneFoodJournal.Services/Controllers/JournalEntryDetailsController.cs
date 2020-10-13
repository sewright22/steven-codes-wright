using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypeOneFoodJournal.Business;
using TypeOneFoodJournal.Models;

namespace TypeOneFoodJournal.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalEntryDetailsController : ControllerBase
    {
        private readonly IJournalEntryManager journalEntryManager;

        public JournalEntryDetailsController(IJournalEntryManager journalEntryManager)
        {
            this.journalEntryManager = journalEntryManager;
        }

        [Authorize]
        [HttpGet("v2/{id}")]
        public async Task<ActionResult<JournalEntryDetails>> Get(int id)
        {
            var retVal = new JournalEntryDetails();

            try
            {
                var journalEntries = this.journalEntryManager.GetJournalEntries();

                var journalEntry = await journalEntries.FirstOrDefaultAsync(x => x.Id == id);

                if (journalEntry is null)
                {
                    return this.NotFound();
                }

                var dose = journalEntry.GetDose();
                retVal.Id = journalEntry.Id;
                retVal.Title = journalEntry.Title;
                retVal.Tags = journalEntry.GetTagsAsString();
                retVal.CarbCount = journalEntry.GetCarbCount();
                retVal.Notes = journalEntry.Notes;
                retVal.Logged = journalEntry.Logged;
                retVal.InsulinAmount = dose.InsulinAmount;
                retVal.TimeExtended = dose.TimeExtended;
                retVal.UpFrontPercent = dose.UpFront;
                retVal.UpFrontAmount = dose.GetUpFrontUnits();
                retVal.ExtendedPercent = dose.Extended;
                retVal.ExtendedAmount = dose.GetExtendedAsUnits();
                retVal.TimeOffset = dose.TimeOffset;
                retVal.TimeExtendedHours = dose.GetTimeExtendedHours();
                retVal.TimeExtendedMinutes = dose.GetTimeExtendedMinutes();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(retVal);
        }

        [Authorize]
        [HttpPost("v2")]
        public async Task<ActionResult<int>> Post([FromBody] JournalEntryDetails journalEntryDetails, int userId)
        {
            try
            {
                var journalEntry = await this.journalEntryManager.CreateNewJournalEntry();
                journalEntry.Title = journalEntryDetails.Title;
                journalEntry.Logged = DateTime.Now;
                var journalEntryTags = journalEntry.AddTagsFromString(journalEntryDetails.Tags);
                var journalEntryNutritionalInfo = journalEntry.AddNutritionalInfo(journalEntryDetails.CarbCount.HasValue ? journalEntryDetails.CarbCount.Value : 0);
                var journalEntryDose = journalEntry.AddDose(journalEntryDetails.InsulinAmount, journalEntryDetails.UpFrontPercent, journalEntryDetails.ExtendedPercent, journalEntryDetails.TimeExtended, journalEntryDetails.TimeOffset);
                var newJeID = await this.journalEntryManager.Save(journalEntryDose, journalEntryNutritionalInfo, journalEntryTags);
                return this.CreatedAtAction("Post", new { id = journalEntry.Id });
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
