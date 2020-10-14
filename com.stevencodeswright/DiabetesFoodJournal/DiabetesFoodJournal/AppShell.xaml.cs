using DiabetesFoodJournal.Pages;
using DiabetesFoodJournal.ViewModels;
using DiabetesFoodJournal.Views;
using DiabetesFoodJournal.Views.JournalEntry;
using DiabetesFoodJournal.Views.JournalEntryUpdate;
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
            Routing.RegisterRoute("journalEntry/update", typeof(JournalEntryUpdateView));
            Routing.RegisterRoute("journalEntry/details", typeof(JournalEntryView));
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
