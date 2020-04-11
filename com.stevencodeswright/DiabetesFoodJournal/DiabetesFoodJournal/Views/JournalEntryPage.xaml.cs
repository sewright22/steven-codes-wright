using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesFoodJournal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JournalEntryPage : ContentPage
    {
        private int stepValue = 5;
        public JournalEntryPage()
        {
            InitializeComponent();
        }

        private void timeSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

            
        }
    }
}