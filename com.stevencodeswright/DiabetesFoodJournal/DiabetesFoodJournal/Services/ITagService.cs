using System.Collections.Generic;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagModel>> GetTags();
    }
}