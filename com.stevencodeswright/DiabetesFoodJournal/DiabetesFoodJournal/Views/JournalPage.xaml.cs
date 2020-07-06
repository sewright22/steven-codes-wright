using DiabetesFoodJournal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JournalPage : ContentPage
    {
        private System.Timers.Timer timer;

        public JournalPage()
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
                //this.loadingAnimation.IsRunning = false;
                //this.loadingAnimation.IsVisible = false;
            });
        }

        private void searchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this.loadingAnimation.IsRunning = true;
            //this.loadingAnimation.IsVisible = true;
            this.timer.Stop();
            //if (string.IsNullOrEmpty(this.searchEntry.Text.TrimEnd()) == false)
            {
                this.timer.Start();
            }
        }
    }
}