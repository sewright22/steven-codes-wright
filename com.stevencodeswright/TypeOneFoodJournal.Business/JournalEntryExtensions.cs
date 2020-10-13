using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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

        public static JournalEntry GetJournalEntryByID(this IQueryable<JournalEntry> journalEntries, int id)
        {
            return journalEntries.FirstOrDefault(x => x.Id == id);
        }

        public static Dose GetDose(this JournalEntry journalEntry)
        {
            var dose = journalEntry.JournalEntryDoses.FirstOrDefault().Dose;
            return dose;
        }

        public static IEnumerable<JournalEntryTag> AddTagsFromString(this JournalEntry journalEntry, string tags)
        {
            var retVal = new List<JournalEntryTag>();
            if (string.IsNullOrEmpty(tags))
            {
                return null;
            }

            if (tags.Contains(","))
            {
                var tagArray = tags.Split(",");

                foreach (var tag in tagArray)
                {
                    var newTag = new Tag
                    {
                        Description = tag,
                    };

                    retVal.Add(new JournalEntryTag
                    {
                        JournalEntry = journalEntry,
                        Tag = newTag,
                    });
                }
            }
            else
            {
                var newTag = new Tag
                {
                    Description = tags,
                };

                retVal.Add(new JournalEntryTag
                {
                    JournalEntry = journalEntry,
                    Tag = newTag,
                });
            }

            return retVal;
        }

        public static JournalEntryDose AddDose(this JournalEntry journalEntry, decimal insulinAmount, int upFrontPercent, int extendedPercent, decimal timeExtended, int timeOffset)
        {
            var dose = new Dose
            {
                InsulinAmount = insulinAmount,
                UpFront = upFrontPercent,
                Extended = extendedPercent,
                TimeExtended = timeExtended,
                TimeOffset = timeOffset,
            };

            return new JournalEntryDose
            {
                JournalEntry = journalEntry,
                Dose = dose,
            };
        }

        public static JournalEntryNutritionalInfo AddNutritionalInfo(this JournalEntry journalEntry, int carbCount)
        {
            var nutritionalInfo = new NutritionalInfo
            {
                Carbohydrates = carbCount,
            };

            return new JournalEntryNutritionalInfo
            {
                JournalEntry = journalEntry,
                NutritionalInfo = nutritionalInfo,
            };
        }
    }
}
