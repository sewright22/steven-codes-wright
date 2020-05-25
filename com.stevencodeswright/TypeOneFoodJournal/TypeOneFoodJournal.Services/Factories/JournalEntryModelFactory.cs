using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeOneFoodJournal.Entities;
using TypeOneFoodJournal.Models;

namespace TypeOneFoodJournal.Services.Factories
{
    public class JournalEntryModelFactory : IJournalEntryModelFactory
    {
        public JournalEntryModel Build(JournalEntry journalEntry)
        {
            var currentEntry = new JournalEntryModel();
            currentEntry.Tags = new List<TagModel>();
            currentEntry.Id = journalEntry.Id;
            currentEntry.Logged = journalEntry.Logged;
            currentEntry.Notes = journalEntry.Notes;
            currentEntry.Title = journalEntry.Title;

            if (journalEntry.JournalEntryNutritionalInfos != null && journalEntry.JournalEntryNutritionalInfos.Any())
            {
                var nutrition = journalEntry.JournalEntryNutritionalInfos.FirstOrDefault().NutritionalInfo;
                if (nutrition.Id > 0)
                {
                    currentEntry.NutritionalInfo = new NutritionalInfoModel();
                    currentEntry.NutritionalInfo.Id = nutrition.Id;
                    currentEntry.NutritionalInfo.Calories = nutrition.Calories;
                    currentEntry.NutritionalInfo.Carbohydrates = nutrition.Carbohydrates;
                    currentEntry.NutritionalInfo.Protein = nutrition.Protein;
                }
            }

            if (journalEntry.JournalEntryDoses != null && journalEntry.JournalEntryDoses.Any())
            {
                var dose = journalEntry.JournalEntryDoses.FirstOrDefault().Dose;
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


            if (journalEntry.JournalEntryTags != null && journalEntry.JournalEntryTags.Any())
            {
                foreach (var tag in journalEntry.JournalEntryTags)
                {
                    var tagViewModel = new TagModel();
                    tagViewModel.Id = tag.Tag.Id;
                    tagViewModel.Description = tag.Tag.Description;
                    currentEntry.Tags.Add(tagViewModel);
                }
            }

            return currentEntry;
        }
    }

    public interface IJournalEntryModelFactory
    {
        JournalEntryModel Build(JournalEntry journalEntry);
    }
}
