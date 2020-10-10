using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using TypeOneFoodJournal.Entities;

namespace TypeOneFoodJournal.Business
{
    public static class JournalEntryExtensions
    {
        public static IQueryable<JournalEntry> ContainsStringInTitleOrTag(this IQueryable<JournalEntry> journalEntries, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return journalEntries;
            }
            else
            {
                return journalEntries.Where(je => je.Title.ToUpper().Contains(name) ||
                                                  je.JournalEntryTags.Where(t => t.Tag.Description.ToUpper().Contains(name)).Any());
            }
        }

        public static string GetTagsAsString(this JournalEntry journalEntry)
        {
            var retVal = new StringBuilder();

            var tags = journalEntry.JournalEntryTags;

            foreach (var jet in tags)
            {
                var tag = jet.Tag.Description;

                retVal.Append(tag);
                retVal.Append(", ");
            }

            if (retVal.Length > 4)
            {
                retVal.Remove(retVal.Length - 2, 2);
            }

            return retVal.ToString();
        }

        public static int? GetCarbCount(this JournalEntry journalEntry)
        {
            var retVal = null as int?;

            if (journalEntry.JournalEntryNutritionalInfos != null && journalEntry.JournalEntryNutritionalInfos.Any())
            {
                var nutrition = journalEntry.JournalEntryNutritionalInfos.FirstOrDefault().NutritionalInfo;
                if (nutrition.Id > 0)
                {
                    retVal = nutrition.Carbohydrates;
                }
            }

            return retVal;
        }
    }
}
