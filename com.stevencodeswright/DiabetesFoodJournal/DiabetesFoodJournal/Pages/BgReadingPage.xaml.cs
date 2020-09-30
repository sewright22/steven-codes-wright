using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesFoodJournal.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BgReadingPage : ContentPage
    {
        public BgReadingPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            if (this.readingsView.LogAgainIsOpen)
            {
                this.readingsView.CloseMenu();
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }
    }
}