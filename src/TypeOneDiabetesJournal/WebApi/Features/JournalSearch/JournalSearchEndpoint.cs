using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Services;
using WebApi.Extensions;

namespace WebApi.Features.JournalSearch
{
    public class JournalSearchEndpoint : Endpoint<JournalSearchRequest, List<JournalEntrySummary>>
    {
        public JournalSearchEndpoint(sewright22_foodjournalContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public sewright22_foodjournalContext DbContext { get; }
        public IMapper Mapper { get; }

        public override void Configure()
        {
            this.Get("api/journalEntries");
            this.AllowAnonymous();
        }

        public override async Task<List<JournalEntrySummary>> ExecuteAsync(JournalSearchRequest req, CancellationToken ct)
        {
            if (req.SearchValue == null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            var journalSearchEntries = this.DbContext.Journalentries
                .SearchTitleAndTag(req.SearchValue)
                .ProjectTo<JournalEntrySummary>(this.Mapper.ConfigurationProvider);

            return journalSearchEntries.ToList();
        }
    }
}
