using DiabetesFoodJournal.Pages;
using DiabetesFoodJournal.Views;
using System;
using System.Collections.Generic;
using Xamarin.Auth;
using Xamarin.Forms;

namespace DiabetesFoodJournal
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("Login", typeof(LoginPage));
            Routing.RegisterRoute("CreateAccount", typeof(CreateAccountPage));
            Routing.RegisterRoute("journalEntry", typeof(JournalEntryPage));
            Routing.RegisterRoute("BgReadings", typeof(BgReadingPage));
        }

        protected override bool OnBackButtonPressed()
        {
            var currentPage = (Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;

            if (currentPage.GetType() == typeof(BgReadingPage))
            {
                return currentPage.SendBackButtonPressed();
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }
    }
}
