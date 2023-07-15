using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace FoodJournal.UI.Shared
{
    public partial class JournalEntries
    {
        [Parameter]
        public string Title { get; set; }
    }
}
