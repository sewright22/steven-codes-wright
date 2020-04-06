using DiabetesFoodJournal.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DiabetesFoodJournal
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("journalEntry", typeof(JournalEntryPage));
        }
    }
}
