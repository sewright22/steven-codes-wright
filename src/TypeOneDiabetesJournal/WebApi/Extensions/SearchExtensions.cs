using DataLayer.Data;

namespace WebApi.Extensions
{
    public static class SearchExtensions
    {
        public static IQueryable<Journalentry> SearchTitleAndTag(this IQueryable<Journalentry> journalentries, string searchValue)
        {
            return journalentries.Where(x => x == null ? false :
                    x.Title == null ? false :
                    x.Title.ToLower()
                        .Contains(searchValue.ToLower())
                        || x.JournalEntryTags == null ? false :
                             x.JournalEntryTags.Any(x => x.Tag == null ? false :
                             x.Tag.Description == null ? false :
                             x.Tag.Description.ToLower().Contains(searchValue.ToLower())));
        }

        public static IQueryable<Journalentry> SearchTitles(this IQueryable<Journalentry> journalentries, string searchValue)
        {
            return journalentries.Where(x => x == null ? false :
                    x.Title == null ? false :
                    x.Title.ToLower()
                        .Contains(searchValue.ToLower()));
        }

        public static IQueryable<Journalentry> SearchTags(this IQueryable<Journalentry> journalentries, string searchValue)
        {
            return journalentries.Where(x => x == null ? false :
                    x.JournalEntryTags == null ? false :
                             x.JournalEntryTags.Any(x => x.Tag == null ? false :
                             x.Tag.Description == null ? false :
                             x.Tag.Description.ToLower().Contains(searchValue.ToLower())));
        }
    }
}
