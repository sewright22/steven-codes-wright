using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.Services
{
    public class TagService : ITagService
    {
        private readonly IWebService webService;

        public TagService(IWebService webService)
        {
            this.webService = webService ?? throw new ArgumentNullException(nameof(webService));
        }

        public Task<IEnumerable<TagModel>> GetTags()
        {
            return this.webService.GetTags("");
        }
    }
}
