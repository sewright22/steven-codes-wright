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
        private decimal upFrontAmount;
        private decimal extendedAmount;
        private int timeExtendedHours;
        private int timeExtendedMinutes;

        public DoseDataModel()
        {
            this.PropertyChanged += DoseDataModel_PropertyChanged;
        }

        private void DoseDataModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.InsulinAmount) ||
                e.PropertyName == nameof(this.UpFront) ||
                e.PropertyName == nameof(this.Extended))
            {
                CalculateAmounts();
            }
            else if (e.PropertyName == nameof(this.UpFrontAmount) ||
                     e.PropertyName == nameof(this.ExtendedAmount))
            {
                this.CalculatePercents();
            }
            
            if (e.PropertyName == nameof(this.UpFront))
            {
                this.Extended = 100 - this.UpFront;
            }
            else if (e.PropertyName == nameof(this.Extended))
            {
                this.UpFront = 100 - this.Extended;
            }
            else if (e.PropertyName == nameof(this.TimeExtended))
            {
                this.TimeExtendedHours = ((int)this.TimeExtended) / 60;
                this.TimeExtendedMinutes = ((int)this.TimeExtended) % 60;
            }
        }

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
        public decimal UpFrontAmount { get { return this.upFrontAmount; } set { SetProperty(ref this.upFrontAmount, value); } }
        public decimal ExtendedAmount { get { return this.extendedAmount; } set { SetProperty(ref this.extendedAmount, value); } }
        public decimal TimeExtended { get { return this.timeExtended; } set { SetProperty(ref this.timeExtended, value); } }
        public int TimeExtendedHours { get { return this.timeExtendedHours; } set { SetProperty(ref this.timeExtendedHours, value); } }
        public int TimeExtendedMinutes { get { return this.timeExtendedMinutes; } set { SetProperty(ref this.timeExtendedMinutes, value); } }
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
            this.CalculateAmounts();
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

        private void CalculateAmounts()
        {
            var upFrontPercentAsDecimal = this.UpFront / (decimal)100;
            var extendedPercentAsDecimal = this.Extended / (decimal)100;
            this.UpFrontAmount = this.InsulinAmount * upFrontPercentAsDecimal;
            this.ExtendedAmount = this.InsulinAmount * extendedPercentAsDecimal;
        }

        private void CalculatePercents()
        {
            if (this.UpFrontAmount > 0)
            {
                this.UpFront = (int)Math.Round((this.UpFrontAmount / this.InsulinAmount) * 100);
            }
        }
    }

}