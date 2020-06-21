using DiabetesFoodJournal.Data;
using DiabetesFoodJournal.DataServices;
using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.Factories;
using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Services;
using DiabetesFoodJournal.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
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
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<CreateAccountViewModel>();
            SimpleIoc.Default.Register<JournalViewModel>();
            SimpleIoc.Default.Register<JournalEntryViewModel>();
            SimpleIoc.Default.Register<JournalEntryHistoryViewModel>();
            SimpleIoc.Default.Register<BgReadingsViewModel>();
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
    }
}
