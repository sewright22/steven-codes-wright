using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public interface IAppDataService
    {
        Task<IEnumerable<JournalEntryDataModel>> SearchJournal(int userId, string searchString);
        Task<JournalEntryDataModel> SaveEntry(JournalEntryDataModel entryToSave, int userId);
        Task<int> SaveDose(DoseDataModel doseToSave);
        Task<int> SaveNurtritionalInfo(NutritionalInfoDataModel nutritionalInfoToSave);
        Task<IEnumerable<Tag>> GetTags(string tagSearchText);
        Task<int> AddNewTag(Tag tag);
        Task<UserDataModel> Login(string email, string password);
    }
}