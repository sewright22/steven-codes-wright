using DiabetesFoodJournal.Models;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.DataModels
{
    public class JournalEntryDataModel : ObservableObject, IDataModel<JournalEntry>
    {
        private int id;
        private DateTime logged;
        private string notes;
        private string title;

        [JsonIgnore]
        public JournalEntry Model
        {
            get;
            protected set;

        }
        public int Id { get { return this.id; } set { SetProperty(ref this.id, value); } }
        public DateTime Logged { get { return this.logged; } set { SetProperty(ref this.logged, value); } }
        public string Notes { get { return this.notes; } set { SetProperty(ref this.notes, value); } }
        public string Title { get { return this.title; } set { SetProperty(ref this.title, value); } }

        [JsonIgnore]
        public string Group
        {
            get
            {
                return string.Format("{0} {1}", Logged.ToString("MMMM"), Logged.Year.ToString());
            }
        }

        public bool IsChanged
        {
            get
            {
                return this.Model.Id != this.id ||
                this.Model.Logged != this.logged ||
                this.Model.Notes != this.notes ||
                this.Model.Title != this.title;
            }
        }

        public void Load(JournalEntry model)
        {
            this.id = model.Id;
            this.logged = model.Logged;
            this.notes = model.Notes;
            Model = model;
        }

        public JournalEntry Save()
        {
            if (Model == null)
            {
                Model = new JournalEntry();

            }

            this.Model.Id = this.id;
            this.Model.Logged = this.logged;
            this.Model.Notes = this.notes;

            return this.Model;
        }
    }

}




