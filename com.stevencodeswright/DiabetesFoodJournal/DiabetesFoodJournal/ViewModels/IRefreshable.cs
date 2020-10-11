using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.ViewModels
{
    public interface IRefreshable
    {
        Task Refresh();
    }
}
