using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesFoodJournal.Views.JournalEntry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JournalEntryView : ContentPage
    {
        public JournalEntryView()
        {
            InitializeComponent();
            this.LogAgainIsOpen = false;
        }

        public bool LogAgainIsOpen { get; private set; }

        private void logAgainButton_Clicked(object sender, EventArgs e)
        {
            this.OpenMenu();
        }

        public void OpenMenu()
        {
            this.LogAgainIsOpen = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await this.hiddenGrid.TranslateTo(this.hiddenGrid.TranslationX, this.bgChart.Height).ConfigureAwait(false);
            });
        }

        public void CloseMenu()
        {
            this.LogAgainIsOpen = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await this.hiddenGrid.TranslateTo(this.hiddenGrid.TranslationX, this.EntryView.Height).ConfigureAwait(false);
            });
        }
    }
}