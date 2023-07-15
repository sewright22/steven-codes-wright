using DiabetesFoodJournal.Entities;
using DiabetesFoodJournal.Models;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.DataModels
{
    public class DoseDataModel : ObservableObject, IDataModel<Dose>
    {
        private int id;
        private int upFront;
        private int extended;
        private decimal timeExtended;
        private int timeOffset;
        private decimal insulinAmount;

        [JsonIgnore]
        public Dose Model
        {
            get;
            protected set;

        }
        public int Id { get { return this.id; } set { SetProperty(ref this.id, value); } }
        public decimal InsulinAmount { get { return this.insulinAmount; } set { SetProperty(ref this.insulinAmount, value); } }
        public int UpFront { get { return this.upFront; } set { SetProperty(ref this.upFront, value); } }
        public int Extended { get { return this.extended; } set { SetProperty(ref this.extended, value); } }
        public decimal TimeExtended { get { return this.timeExtended; } set { SetProperty(ref this.timeExtended, value); } }
        public int TimeOffset 
        { 
            get { return this.timeOffset; } 
            set 
            {
                var newStep = (int)Math.Round((double)value / 5);

                SetProperty(ref this.timeOffset, newStep * 5);
            } 
        }

        [JsonIgnore]
        public bool IsChanged
        {
            get
            {
                return this.Model.Id != this.id ||
                this.Model.InsulinAmount != this.insulinAmount ||
                this.Model.UpFront != this.upFront ||
                this.Model.Extended != this.extended ||
                this.Model.TimeExtended != this.timeExtended ||
                this.Model.TimeOffset != this.timeOffset;
            }
        }

        public void Load(Dose model)
        {
            this.id = model.Id;
            this.insulinAmount = model.InsulinAmount;
            this.upFront = model.UpFront;
            this.extended = model.Extended;
            this.timeExtended = model.TimeExtended;
            this.timeOffset = model.TimeOffset;
            Model = model;
        }

        public Dose Copy()
        {
            var retVal = new Dose();

            retVal.InsulinAmount = this.insulinAmount;
            retVal.UpFront = this.upFront;
            retVal.Extended = this.extended;
            retVal.TimeExtended = this.timeExtended;
            retVal.TimeOffset = this.timeOffset;

            return retVal;
        }

        public Dose Save()
        {
            if (Model == null)
            {
                Model = new Dose();

            }

            this.Model.Id = this.id;
            this.Model.InsulinAmount = this.insulinAmount;
            this.Model.UpFront = this.upFront;
            this.Model.Extended = this.extended;
            this.Model.TimeExtended = this.timeExtended;
            this.Model.TimeOffset = this.timeOffset;

            return this.Model;
        }
    }

}