﻿using DiabetesFoodJournal.DataModels;
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

        public MockAppDataService(IDataStore<JournalEntry> journalEntries, IDataStore<Tag> tags, IDataStore<NutritionalInfo> nutritionalInfos, IDataStore<JournalEntryTag> journalEntryTags, IDataStore<JournalEntryNutritionalInfo> journalEntryNutritionalInfos)
        {
            this.journalEntries = journalEntries;
            this.tags = tags;
            this.nutritionalInfos = nutritionalInfos;
            this.journalEntryTags = journalEntryTags;
            this.journalEntryNutritionalInfos = journalEntryNutritionalInfos;
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
                          where entry.Title.ToUpper().Contains(searchString.ToUpper()) || tag.Description.ToUpper().Contains(searchString.ToUpper())
                          select new
                          {
                              entry,
                              tag,
                              nutrition
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
    }
}