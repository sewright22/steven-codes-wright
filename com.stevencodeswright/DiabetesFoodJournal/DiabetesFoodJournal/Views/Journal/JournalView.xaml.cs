using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesFoodJournal.Views.Journal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JournalView : ContentPage
    {
        private Timer timer;

        public JournalView()
        {
            InitializeComponent();
            this.timer = new System.Timers.Timer(500);
            this.timer.Elapsed += this.Timer_Elapsed;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.searchButton.Command.Execute(this.searchEntry.Text ?? "");
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.timer.Stop();
                this.searchButton.Command.Execute(this.searchEntry.Text.Trim());
            });
        }

        private void searchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.timer.Stop();
            {
                this.timer.Start();
            }
        }

        private void searchEntry_Focused(object sender, FocusEventArgs e)
        {
            this.searchEntry.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeWord);
        }
    }
}