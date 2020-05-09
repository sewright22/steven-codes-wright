using DiabetesFoodJournal.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public interface IAppDataService
    {
        Task<IEnumerable<JournalEntryDataModel>> SearchJournal(string searchString);
        Task<int> SaveEntry(JournalEntryDataModel entryToSave);
        Task<int> SaveDose(DoseDataModel doseToSave);
        Task<int> SaveNurtritionalInfo(NutritionalInfoDataModel nutritionalInfoToSave);
    }
}