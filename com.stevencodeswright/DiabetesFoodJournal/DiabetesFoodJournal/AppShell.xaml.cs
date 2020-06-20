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
            Routing.RegisterRoute("journalEntry", typeof(FoodDetailsPage));
        }
    }
}
