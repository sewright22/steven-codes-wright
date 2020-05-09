using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.ModelLinks;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using DiabetesFoodJournal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public class MockAppDataService : IAppDataService
    {
        private readonly IDataStore<JournalEntry> journalEntries;
        private readonly IDataStore<Tag> tags;
        private readonly IDataStore<NutritionalInfo> nutritionalInfos;
        private readonly IDataStore<JournalEntryTag> journalEntryTags;
        private readonly IDataStore<JournalEntryNutritionalInfo> journalEntryNutritionalInfos;
        private readonly IDataStore<JournalEntryDose> journalEntryDoses;
        private readonly IDataStore<Dose> doses;

        public MockAppDataService(IDataStore<JournalEntry> journalEntries, IDataStore<Tag> tags, IDataStore<NutritionalInfo> nutritionalInfos, IDataStore<JournalEntryTag> journalEntryTags, IDataStore<JournalEntryNutritionalInfo> journalEntryNutritionalInfos, IDataStore<JournalEntryDose> journalEntryDoses, IDataStore<Dose> doses)       
        {
            this.journalEntries = journalEntries;
            this.tags = tags;
            this.nutritionalInfos = nutritionalInfos;
            this.journalEntryTags = journalEntryTags;
            this.journalEntryNutritionalInfos = journalEntryNutritionalInfos;
            this.journalEntryDoses = journalEntryDoses;
            this.doses = doses;
        }

        public async Task<int> SaveEntry(JournalEntryDataModel entryToSave)
        {
            var retVal = 0;
            var entry = entryToSave.Save();

            if (entry.Id == 0)
            {
                retVal = await this.journalEntries.AddItemAsync(entry);
            }
            else
            {
                await this.journalEntries.UpdateItemAsync(entry);
                retVal = entry.Id;
            }

            entryToSave.Dose.Id = await SaveDose(entryToSave.Dose);
            await SaveJournalEntryDose(entry.Id, entryToSave.Dose.Id);

            entryToSave.NutritionalInfo.Id = await SaveNurtritionalInfo(entryToSave.NutritionalInfo);
            await SaveJournalEntryNutritionalInfo(entry.Id, entryToSave.NutritionalInfo.Id);

            foreach (var tag in entryToSave.Tags)
            {
                await SaveJournalEntryTag(entry.Id, tag.Id);
            }

            return retVal;
        }

        public async Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchString)
        {
            var retVal = new List<JournalEntryDataModel>();

            var results = from entry in await journalEntries.GetItemsAsync()
                          join entryTag in await journalEntryTags.GetItemsAsync() on entry.Id equals entryTag.JournalEntryId into et
                          from entryTag in et.DefaultIfEmpty(new JournalEntryTag() { Id = entry.Id, JournalEntryId = 0, TagId = 0 })
                          join tag in await tags.GetItemsAsync() on entryTag.TagId equals tag.Id into te
                          from tag in te.DefaultIfEmpty(new Tag() { Id = entryTag.TagId, Description = "" })
                          join entryNutrition in await journalEntryNutritionalInfos.GetItemsAsync() on entry.Id equals entryNutrition.JournalEntryId into en
                          from entryNutrition in en.DefaultIfEmpty(new JournalEntryNutritionalInfo() { Id = entry.Id, JournalEntryId = 0, JournalEntryNutritionalInfoId = 0 })
                          join nutrition in await nutritionalInfos.GetItemsAsync() on entryNutrition.JournalEntryNutritionalInfoId equals nutrition.Id into n
                          from nutrition in n.DefaultIfEmpty(new NutritionalInfo() { Id = entryNutrition.JournalEntryNutritionalInfoId, Carbohydrates=0 })
                          join entryDose in await journalEntryDoses.GetItemsAsync() on entry.Id equals entryDose.JournalEntryId into ed
                          from entryDose in ed.DefaultIfEmpty(new JournalEntryDose() { Id = entry.Id, JournalEntryId = 0, DoseId = 0 })
                          join dose in await doses.GetItemsAsync() on entryDose.DoseId equals dose.Id into d
                          from dose in d.DefaultIfEmpty(new Dose() { Id = entryDose.DoseId, InsulinAmount = 0, Extended=0, UpFront=100, TimeExtended=0, TimeOffset=0 })
                          where entry.Title.ToUpper().Contains(searchString.ToUpper()) || tag.Description.ToUpper().Contains(searchString.ToUpper())
                          select new
                          {
                              entry,
                              tag,
                              nutrition,
                              dose
                          };

            var currentEntry = new JournalEntryDataModel();
            foreach (var result in results)
            {
                if (result.entry.Id != currentEntry.Id)
                {
                    if(currentEntry.Id > 0)
                    {
                        retVal.Add(currentEntry);
                    }

                    currentEntry = new JournalEntryDataModel();
                    currentEntry.Load(result.entry);

                    if (result.nutrition.Id > 0)
                    {
                        currentEntry.NutritionalInfo.Load(result.nutrition);
                    }

                    if(result.dose.Id>0)
                    {
                        currentEntry.Dose.Load(result.dose);
                    }
                }

                if (result.tag.Id > 0)
                {
                    var tagViewModel = new TagDataModel();
                    tagViewModel.Load(result.tag);
                    currentEntry.Tags.Add(tagViewModel);
                }
            }

            if (currentEntry.Id > 0)
            {
                retVal.Add(currentEntry);
            }

            return retVal;
        }

        private async Task SaveJournalEntryTag(int journalEntryId, int tagId)
        {
            if ((await journalEntryTags.GetItemsAsync()).FirstOrDefault(x => x.JournalEntryId == journalEntryId && x.TagId == tagId) == null)
            {
                await journalEntryTags.AddItemAsync(new JournalEntryTag { JournalEntryId = journalEntryId, TagId = tagId });
            }
        }

        private async Task SaveJournalEntryNutritionalInfo(int journalEntryId, int nutritionalInfoId)
        {
            if ((await journalEntryNutritionalInfos.GetItemsAsync()).FirstOrDefault(x => x.JournalEntryId == journalEntryId && x.JournalEntryNutritionalInfoId == nutritionalInfoId) == null)
            {
                await journalEntryNutritionalInfos.AddItemAsync(new JournalEntryNutritionalInfo { JournalEntryId = journalEntryId, JournalEntryNutritionalInfoId = nutritionalInfoId });
            }
        }

        private async Task SaveJournalEntryDose(int journalEntryId, int doseId)
        {
            if ((await journalEntryDoses.GetItemsAsync()).FirstOrDefault(x => x.JournalEntryId == journalEntryId && x.DoseId == doseId) == null)
            {
                await journalEntryDoses.AddItemAsync(new JournalEntryDose { JournalEntryId = journalEntryId, DoseId = doseId });
            }
        }

        public async Task<int> SaveDose(DoseDataModel doseToSave)
        {
            var retVal = 0;
            var dose = doseToSave.Save();

            if (dose.Id == 0)
            {
                retVal = await this.doses.AddItemAsync(dose);
            }
            else
            {
                await this.doses.UpdateItemAsync(dose);
                retVal = dose.Id;
            }

            return retVal;
        }

        public async Task<int> SaveNurtritionalInfo(NutritionalInfoDataModel nutritionalInfoToSave)
        {
            var retVal = 0;
            var nutritionalInfo = nutritionalInfoToSave.Save();

            if (nutritionalInfo.Id == 0)
            {
                retVal = await this.nutritionalInfos.AddItemAsync(nutritionalInfo);
            }
            else
            {
                await this.nutritionalInfos.UpdateItemAsync(nutritionalInfo);
                retVal = nutritionalInfo.Id;
            }

            return retVal;
        }
    }
}
