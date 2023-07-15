using DiabetesFoodJournal.DataServices;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.ViewModels
{
    public class CreateAccountViewModel : BaseViewModel
    {
        private readonly ILoginDataService loginDataService;
        private readonly INavigationHelper navigationHelper;
        private string email;
        private string password;
        private string reenterPassword;
        private string messageText;
        private bool errorIsVisible;
        private string passwordsDoNotMatch;

        public CreateAccountViewModel(ILoginDataService loginDataService, INavigationHelper navigationHelper)
        {
            this.loginDataService = loginDataService;
            this.navigationHelper = navigationHelper;
            this.passwordsDoNotMatch = "The passwords you entered do not match.";
            CreateAccountCommand = new AsyncCommand(()=> CreateAccount(), CanCreate);
        }

        public bool ErrorIsVisible { get { return this.errorIsVisible; } set { SetProperty(ref this.errorIsVisible, value); } }
        public string MessageText { get { return this.messageText; } set { SetProperty(ref this.messageText, value); } }
        public string Email { get { return this.email; } set { SetProperty(ref this.email, value); } }
        public string Password { get { return this.password; } set { SetProperty(ref this.password, value); } }
        public string ReEnterPassword { get { return this.reenterPassword; } set { SetProperty(ref this.reenterPassword, value); } }
        public AsyncCommand CreateAccountCommand { get; set; }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);
            CreateAccountCommand.RaiseCanExecuteChanged();
        }

        private bool CanCreate(object arg)
        {
            return string.IsNullOrWhiteSpace(this.email) == false
                && string.IsNullOrWhiteSpace(this.password) == false
                && string.IsNullOrWhiteSpace(this.reenterPassword) == false;
        }

        private async Task CreateAccount()
        {
            ErrorIsVisible = false;
            if (this.password.Equals(this.reenterPassword))
            {
                await this.loginDataService.CreateAccount(this.email, this.password);
                await this.navigationHelper.GoToAsync("///main", true);
            }
            else
            {
                MessageText = this.passwordsDoNotMatch;
                ErrorIsVisible = true;
            }
        }
    }
}
