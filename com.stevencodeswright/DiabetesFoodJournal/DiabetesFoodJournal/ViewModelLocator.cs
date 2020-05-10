using DiabetesFoodJournal.Data;
using DiabetesFoodJournal.DataServices;
using DiabetesFoodJournal.Entities;
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
            SimpleIoc.Default.Register<IAppDataService, MockAppDataService>();
            SimpleIoc.Default.Register<IJournalDataService, JournalDataService>();
            SimpleIoc.Default.Register<IJournalEntryDataService, JournalEntryDataService>();
            SimpleIoc.Default.Register<IJournalEntryHistoryDataService, JournalEntryHistoryDataService>();
            SimpleIoc.Default.Register<JournalViewModel>();
            SimpleIoc.Default.Register<JournalEntryViewModel>();
            SimpleIoc.Default.Register<JournalEntryHistoryViewModel>();
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

        public JournalEntryHistoryViewModel JournalEntryHistory
        {
            get
            {
                return SimpleIoc.Default.GetInstance<JournalEntryHistoryViewModel>();
            }
        }
    }
}
