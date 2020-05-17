using DiabetesFoodJournal.Entities;
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
        private bool isSelected;

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
        public DoseDataModel Dose { get; } = new DoseDataModel();
        public NutritionalInfoDataModel NutritionalInfo { get; } = new NutritionalInfoDataModel();
        public ObservableRangeCollection<TagDataModel> Tags { get; } = new ObservableRangeCollection<TagDataModel>();

        [JsonIgnore]
        public string Group
        {
            get
            {
                if (Logged.Date.Equals(DateTime.Today))
                {
                    return "Today";
                }
                else if (Logged.Date.Equals(DateTime.Today.Add(TimeSpan.FromDays(-1))))
                {
                    return "Yesterday";
                }
                else
                {
                    return string.Format("{0} {1}", Logged.ToString("MM/dd/yyyy"), Logged.Year.ToString());
                }
            }
        }

        [JsonIgnore]
        public bool IsChanged
        {
            get
            {
                var thisObjectIsChanged =  this.Model.Id != this.id ||
                                           this.Model.Logged != this.logged ||
                                           this.Model.Notes != this.notes ||
                                           this.Model.Title != this.title;

                if(thisObjectIsChanged == false)
                {
                    foreach(var tag in Tags)
                    {
                        thisObjectIsChanged = tag.IsChanged;
                        if(thisObjectIsChanged)
                        {
                            break;
                        }
                    }
                }

                return thisObjectIsChanged;
            }
        }

        [JsonIgnore]
        public bool IsSelected { get { return this.isSelected; } set { SetProperty(ref this.isSelected, value); } }


        public void Load(JournalEntry model)
        {
            this.Id = model.Id;
            this.Logged = model.Logged;
            this.Notes = model.Notes;
            this.Title = model.Title;
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
            this.Model.Title = this.title;

            return this.Model;
        }
        public JournalEntry Copy()
        {
            var retVal = new JournalEntry();
            retVal.Title = this.title;

            return retVal;
        }
    }

}




