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
        private float? startingBg;
        private float? highestBg;
        private int? highestBgTimeSpanInMinutes;
        private float? lowestBg;
        private int? lowestBgTimeSpanInMinutes;

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
        public float? StartingBg { get { return this.startingBg; } set { SetProperty(ref this.startingBg, value); } }
        [JsonIgnore]
        public float? HighestBg { get { return this.highestBg; } set { SetProperty(ref this.highestBg, value); } }
        [JsonIgnore]
        public int? HighestBgTimeSpanInMinutes { get { return this.highestBgTimeSpanInMinutes; } set { SetProperty(ref this.highestBgTimeSpanInMinutes, value); } }
        [JsonIgnore]
        public float? LowestBg { get { return this.lowestBg; } set { SetProperty(ref this.lowestBg, value); } }
        [JsonIgnore]
        public int? LowestBgTimeSpanInMinutes { get { return this.lowestBgTimeSpanInMinutes; } set { SetProperty(ref this.lowestBgTimeSpanInMinutes, value); } }
        public DoseDataModel Dose { get; } = new DoseDataModel();
        public NutritionalInfoDataModel NutritionalInfo { get; } = new NutritionalInfoDataModel();
        public ObservableRangeCollection<TagDataModel> Tags { get; } = new ObservableRangeCollection<TagDataModel>();
        public ObservableRangeCollection<GlucoseReading> BgReadings { get; } = new ObservableRangeCollection<GlucoseReading>();

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
                    return string.Format("{0}", Logged.ToString("MM/dd/yyyy"));
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




