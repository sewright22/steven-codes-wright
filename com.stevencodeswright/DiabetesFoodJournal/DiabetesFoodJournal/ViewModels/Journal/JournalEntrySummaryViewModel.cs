using DiabetesFoodJournal.Views.Journal;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TypeOneFoodJournal.Models;

namespace DiabetesFoodJournal.ViewModels.Journal
{
    public class JournalEntrySummaryViewModel : BaseViewModel
    {
        private JournalEntrySummary model;
        private bool isSelected;
        private string group;

        public JournalEntrySummaryViewModel()
        {
        }

        public JournalEntrySummary Model
        {
            get { return this.model; }
            set 
            { 
                if (this.SetProperty(ref this.model, value))
                {
                    this.IsSelected = this.model.IsSelected;
                    this.Group = this.model.Group;
                }
            }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.SetProperty(ref this.isSelected, value); }
        }

        public string Group
        {
            get { return this.group; }
            set { this.SetProperty(ref this.group, value); }
        }
    }
}
