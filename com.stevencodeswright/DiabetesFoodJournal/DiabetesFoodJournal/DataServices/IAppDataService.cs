using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public interface IAppDataService
    {
        Task<IEnumerable<JournalEntryDataModel>> SearchJournal(int userId, string searchString);
        Task<IEnumerable<JournalEntryDataModel>> SearchJournal(int userId, DateTime startTime, DateTime endTime, int idToExclude);
        Task<JournalEntryDataModel> SaveEntry(JournalEntryDataModel entryToSave, int userId);
        Task<int> SaveDose(DoseDataModel doseToSave);
        Task<int> SaveNurtritionalInfo(NutritionalInfoDataModel nutritionalInfoToSave);
        Task<IEnumerable<Tag>> GetTags(string tagSearchText);
        Task<int> AddNewTag(Tag tag);
        Task<UserDataModel> Login(string email, string password);
        Task<UserDataModel> CreateAccount(string email, string password);
    }
}