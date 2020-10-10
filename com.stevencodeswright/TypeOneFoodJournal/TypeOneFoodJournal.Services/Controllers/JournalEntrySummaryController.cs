using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypeOneFoodJournal.Business;
using TypeOneFoodJournal.Data;
using TypeOneFoodJournal.Models;
using TypeOneFoodJournal.Services.DataServices;
using TypeOneFoodJournal.Services.Factories;

namespace TypeOneFoodJournal.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalEntrySummaryController : ControllerBase
    {
        private readonly IJournalEntryManager journalEntryManager;

        public JournalEntrySummaryController(IJournalEntryManager journalEntryManager)
        {
            this.journalEntryManager = journalEntryManager ?? throw new ArgumentNullException(nameof(journalEntryManager));
        }

        [Authorize]
        [HttpGet("v2")]
        public async Task<ActionResult<IEnumerable<JournalEntrySummary>>> Get([FromQuery] int userId,
                                                                              [FromQuery] string searchValue = "",
                                                                              [FromQuery] DateTime? startTime = null,
                                                                              [FromQuery] DateTime? endTime = null,
                                                                              [FromQuery] int? idToExclude = null)
        {
            var retVal = new List<JournalEntrySummary>();

            try
            {
                var journalEntries = this.journalEntryManager.GetJournalEntriesForUserId(userId);

                if (idToExclude.HasValue && startTime.HasValue && endTime.HasValue)
                {
                    journalEntries = journalEntries.Where(je => je.Id != idToExclude && je.Logged >= startTime.Value && je.Logged <= endTime.Value);
                }
                else
                {
                    var upperSearchValue = searchValue.ToUpper();
                    journalEntries = journalEntries.ContainsStringInTitleOrTag(upperSearchValue);
                }

                if (await journalEntries.AnyAsync())
                {
                    foreach (var result in await journalEntries.OrderByDescending(je => je.Logged).Take(10).ToListAsync())
                    {
                        var group = this.DetermineGroup(result.Logged);

                        retVal.Add(new JournalEntrySummary
                        {
                            ID = result.Id,
                            DateLogged = result.Logged,
                            Title = result.Title,
                            Tags = result.GetTagsAsString(),
                            CarbCount = result.GetCarbCount(),
                            IsSelected = false,
                            Group = group,
                        });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(retVal);
        }

        private string DetermineGroup(DateTime logged)
        {
            if (logged.Date.Equals(DateTime.Today))
            {
                return "Today";
            }
            else if (logged.Date.Equals(DateTime.Today.Add(TimeSpan.FromDays(-1))))
            {
                return "Yesterday";
            }
            else
            {
                return string.Format("{0}", logged.ToString("MM/dd/yyyy"));
            }
        }
    }
}
