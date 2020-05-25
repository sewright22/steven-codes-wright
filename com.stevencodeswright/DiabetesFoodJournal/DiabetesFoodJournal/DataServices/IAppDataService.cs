using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public interface IAppDataService
    {
        Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchString);
        Task<JournalEntryDataModel> SaveEntry(JournalEntryDataModel entryToSave);
        Task<int> SaveDose(DoseDataModel doseToSave);
        Task<int> SaveNurtritionalInfo(NutritionalInfoDataModel nutritionalInfoToSave);
        Task<IEnumerable<Tag>> GetTags(string tagSearchText);
        Task<int> AddNewTag(Tag tag);
    }
}