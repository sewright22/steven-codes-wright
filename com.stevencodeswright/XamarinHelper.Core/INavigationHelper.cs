using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinHelper.Core
{
    public interface INavigationHelper
    {
        Task GoToAsync(string path, bool animate = true);
    }
}
