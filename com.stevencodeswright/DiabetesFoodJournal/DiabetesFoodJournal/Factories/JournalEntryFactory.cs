using DiabetesFoodJournal.DataModels;
using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.WebApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Factories
{
    public class JournalEntryFactory : IJournalEntryFactory
    {
        public JournalEntry Build()
        {
            return new JournalEntry();
        }

        public JournalEntry Build(JournalEntryWebApiModel journalEntryWebApiModel)
        {
            var retVal = Build();

            retVal.Id = journalEntryWebApiModel.Id;
            retVal.Logged = journalEntryWebApiModel.Logged;
            retVal.Notes = journalEntryWebApiModel.Notes;
            retVal.Title = journalEntryWebApiModel.Title;

            return retVal;
        }
    }

    public interface IJournalEntryFactory
    {
        JournalEntry Build();
        JournalEntry Build(JournalEntryWebApiModel journalEntryWebApiModel);
    }
}
