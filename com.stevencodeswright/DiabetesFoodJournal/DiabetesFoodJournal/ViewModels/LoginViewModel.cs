using DiabetesFoodJournal.DataServices;
using DiabetesFoodJournal.Messages;
using GalaSoft.MvvmLight.Messaging;
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
        private readonly IMessenger messenger;
        private string email;

        public LoginViewModel(ILoginDataService loginDataService, INavigationHelper navigationHelper, IMessenger messenger)
        {
            this.loginDataService = loginDataService;
            this.navigationHelper = navigationHelper;
            this.messenger = messenger;
            LoginCommand = new AsyncCommand<string>(password=>Login(password), CanLogin);
            CreateCommand = new AsyncCommand(()=>CreateAccount());

            this.messenger.Register<AppLoadedMessage>(this, async (message) => await this.AppLoaded(message).ConfigureAwait(false));
        }

        private async Task AppLoaded(AppLoadedMessage message)
        {
            this.Email = await this.loginDataService.GetUserName();
            var password = await this.loginDataService.GetPassword();

            if(!string.IsNullOrEmpty(this.Email) && !string.IsNullOrEmpty(password))
            {
                await this.Login(password).ConfigureAwait(false);
            }
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
