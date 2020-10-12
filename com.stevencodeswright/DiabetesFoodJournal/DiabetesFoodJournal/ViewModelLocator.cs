using DiabetesFoodJournal.Data;
using DiabetesFoodJournal.DataServices;
using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.Factories;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using DiabetesFoodJournal.ViewModels;
using DiabetesFoodJournal.ViewModels.JournalEntry;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using XamarinHelper.Core;

namespace DiabetesFoodJournal
{
    class ViewModelLocator
    {
        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<INavigationHelper, ShellNavigation>();
            SimpleIoc.Default.Register(() => { return Messenger.Default; });
            SimpleIoc.Default.Register<IDatabaseSettings, DefaultDatabaseSettings>();
            SimpleIoc.Default.Register<ISqlLiteAsyncConnectionFactory, SqlLiteAsyncConnectionFactory>();
            SimpleIoc.Default.Register<IHashHelper, HashHelper>();
            SimpleIoc.Default.Register<IFoodJournalDatabase, FoodJournalDatabase>();
            SimpleIoc.Default.Register<ISecureStorage, SecureStorageHelper>();
            SimpleIoc.Default.Register<IUserInfo, UserInfoHelper>();
            SimpleIoc.Default.Register<IDexcomDataStore, DexcomDataStore>();
            SimpleIoc.Default.Register<IDataStore<JournalEntry>, LocalJournalEntryDataStore>();
            //SimpleIoc.Default.Register<IDataStore<GlucoseReading>, MockReadingDataStore>();
            SimpleIoc.Default.Register<IDataStore<Dose>, LocalDoseDataStore>();
            SimpleIoc.Default.Register<IDataStore<Tag>, LocalTagDataStore>();
            SimpleIoc.Default.Register<IDataStore<NutritionalInfo>, LocalNutritionalInfoDataStore>();
            SimpleIoc.Default.Register<IDataStore<JournalEntryTag>, LocalJournalEntryTagDataStore>();
            SimpleIoc.Default.Register<IDataStore<JournalEntryNutritionalInfo>, LocalJournalEntryNutritionalInfoDataStore>();
            SimpleIoc.Default.Register<IDataStore<JournalEntryDose>, LocalJournalEntryDoseDataStore>();
            //SimpleIoc.Default.Register<IAppDataService, MockAppDataService>();
            SimpleIoc.Default.Register<IDoseFactory, DoseFactory>();
            SimpleIoc.Default.Register<IJournalEntryFactory, JournalEntryFactory>();
            SimpleIoc.Default.Register<INutritionalInfoFactory, NutritionalInfoFactory>();
            SimpleIoc.Default.Register<ITagFactory, TagFactory>();
            SimpleIoc.Default.Register<IAppDataService, WebApiDataService>();
            SimpleIoc.Default.Register<ILoginDataService, LoginDataService>();
            SimpleIoc.Default.Register<IJournalDataService, JournalDataService>();
            SimpleIoc.Default.Register<IJournalEntryDataService, JournalEntryDataService>();
            SimpleIoc.Default.Register<IJournalEntryHistoryDataService, JournalEntryHistoryDataService>();
            SimpleIoc.Default.Register<IBgReadingsDataService, BgReadingsDataService>();
            SimpleIoc.Default.Register<IWebService, WebService>();
            SimpleIoc.Default.Register<IJournalEntrySummaryService, JournalEntrySummaryService>();
            SimpleIoc.Default.Register<IJournalEntryDetailsService, JournalEntryDetailsService>();
            SimpleIoc.Default.Register<IBloodSugarService, BloodSugarService>();
            SimpleIoc.Default.Register<IMessagingCenter, MessagingCenter>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<CreateAccountViewModel>();
            SimpleIoc.Default.Register<JournalViewModel>();
            SimpleIoc.Default.Register<JournalEntryViewModel>();
            SimpleIoc.Default.Register<JournalEntryDetailsViewModel>();
            SimpleIoc.Default.Register<JournalEntryHistoryViewModel>();
            SimpleIoc.Default.Register<BloodSugarReadingsViewModel>();
            SimpleIoc.Default.Register<AdvancedBloodSugarStatsViewModel>();
            SimpleIoc.Default.Register<LogAgainViewModel>();
        }

        public static readonly BindableProperty AutoWireViewModelProperty =
           BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public LoginViewModel Login
        {
            get
            {
                return SimpleIoc.Default.GetInstance<LoginViewModel>();
            }
        }

        public CreateAccountViewModel CreateAccount
        {
            get
            {
                return SimpleIoc.Default.GetInstance<CreateAccountViewModel>();
            }
        }
        public JournalViewModel Journal
        {
            get
            {
                return SimpleIoc.Default.GetInstance<JournalViewModel>();
            }
        }

        public JournalEntryViewModel JournalEntry
        {
            get
            {
                return SimpleIoc.Default.GetInstance<JournalEntryViewModel>();
            }
        }

        public BgReadingsViewModel BgReadings
        {
            get
            {
                return SimpleIoc.Default.GetInstance<BgReadingsViewModel>();
            }
        }

        public JournalEntryHistoryViewModel JournalEntryHistory
        {
            get
            {
                return SimpleIoc.Default.GetInstance<JournalEntryHistoryViewModel>();
            }
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(
                CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = SimpleIoc.Default.GetInstance(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
