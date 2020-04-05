using DiabetesFoodJournal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JournalPage : ContentPage
    {
        public JournalPage()
        {
            InitializeComponent();

            BindingContext = new JournalViewModel(DependencyService.Resolve<IDeviceHelper>());
        }
    }
}