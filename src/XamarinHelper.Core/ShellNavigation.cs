using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinHelper.Core
{
    public class ShellNavigation : INavigationHelper
    {
        public Task GoToAsync(string path, bool animate = true)
        {
            return Shell.Current.GoToAsync(path, animate);
        }
    }
}
