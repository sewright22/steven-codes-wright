using DiabetesFoodJournal.DataServices;
using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.ModelLinks;
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
            SimpleIoc.Default.Register<ISecureStorage, SecureStorageHelper>();
            SimpleIoc.Default.Register<IUserInfo, UserInfoHelper>();
            SimpleIoc.Default.Register<IDexcomDataStore, DexcomDataStore>();
            SimpleIoc.Default.Register<IDataStore<JournalEntry>, MockJournalEntryDataStore>();
            SimpleIoc.Default.Register<IDataStore<GlucoseReading>, MockReadingDataStore>();
            SimpleIoc.Default.Register<IDataStore<Dose>, MockDoseDataStore>();
            SimpleIoc.Default.Register<IDataStore<Tag>, MockTagDataStore>();
            SimpleIoc.Default.Register<IDataStore<NutritionalInfo>, MockNutritionalInfoDataStore>();
            SimpleIoc.Default.Register<IDataStore<JournalEntryTag>, MockJournalEntryTagDataStore>();
            SimpleIoc.Default.Register<IDataStore<JournalEntryNutritionalInfo>, MockJournalEntryNutritionalInfoDataStore>();
            SimpleIoc.Default.Register<IDataStore<JournalEntryDose>, MockJournalEntryDoseDataStore>();
            SimpleIoc.Default.Register<IAppDataService, MockAppDataService>();
            SimpleIoc.Default.Register<IJournalDataService, JournalDataService>();
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
