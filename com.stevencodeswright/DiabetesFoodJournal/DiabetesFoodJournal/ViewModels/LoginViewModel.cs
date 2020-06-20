using DiabetesFoodJournal.DataServices;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginDataService loginDataService;
        private readonly INavigationHelper navigationHelper;
        private string email;

        public LoginViewModel(ILoginDataService loginDataService, INavigationHelper navigationHelper)
        {
            this.loginDataService = loginDataService;
            this.navigationHelper = navigationHelper;
            LoginCommand = new AsyncCommand<string>(password=>Login(password), CanLogin);
            CreateCommand = new AsyncCommand(()=>CreateAccount());
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.OnPropertyChanged(propertyName);
            this.LoginCommand.RaiseCanExecuteChanged();
        }

        private bool CanLogin(object arg)
        {
            return this.IsNotBusy && !string.IsNullOrWhiteSpace(this.email);
        }

        private async Task CreateAccount()
        {
            await this.navigationHelper.GoToAsync("CreateAccount");
        }

        private async Task Login(string password)
        {
            if (this.IsNotBusy)
            {
                this.IsBusy = true;
                var success = await this.loginDataService.Login(this.email, password);

                if (success)
                {
                    await this.navigationHelper.GoToAsync("///main", true);
                }
                else
                {

                }
                this.IsBusy = false;
            }
        }

        public string Email { get { return this.email; } set { SetProperty(ref this.email, value); } }
        public AsyncCommand<string> LoginCommand { get; set; }
        public AsyncCommand CreateCommand { get; set; }
    }
}
