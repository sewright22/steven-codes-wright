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
        public JournalEntryPage()
        {
            InitializeComponent();
        }

        private void TagSearchEntry_Focused(object sender, FocusEventArgs e)
        {
            this.TagSearchEntry.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
        }

        private void TitleEntry_Focused(object sender, FocusEventArgs e)
        {
            this.TitleEntry.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
        }
    }
}