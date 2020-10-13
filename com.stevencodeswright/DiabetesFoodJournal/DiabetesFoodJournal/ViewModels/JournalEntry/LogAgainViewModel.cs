using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;
using Xamarin.Forms;

namespace DiabetesFoodJournal.ViewModels.JournalEntry
{
    public class LogAgainViewModel : BaseViewModel
    {
        private readonly IMessagingCenter messagingCenter;
        private decimal insulinAmount;
        private decimal upFrontAmount;
        private decimal extendedAmount;
        private decimal timeExtended;
        private int upFrontPercent;
        private int extendedPercent;
        private int timeExtendedHours;
        private int timeExtendedMinutes;

        public LogAgainViewModel(IMessagingCenter messagingCenter)
        {
            this.messagingCenter = messagingCenter ?? throw new ArgumentNullException(nameof(messagingCenter));

            this.messagingCenter.Subscribe<JournalEntryDetails>(this, "LogAgainClicked", this.Refresh);
        }

        public decimal InsulinAmount
        {
            get { return this.insulinAmount; }
            set { this.SetProperty(ref this.insulinAmount, value, nameof(this.InsulinAmount), this.UpdateInsulinPercents); }
        }

        public decimal UpFrontAmount
        {
            get { return this.upFrontAmount; }
            set { this.SetProperty(ref this.upFrontAmount, value, nameof(this.UpFrontAmount), this.UpdateInsulinPercents); }
        }

        public decimal ExtendedAmount
        {
            get { return this.extendedAmount; }
            set { this.SetProperty(ref this.extendedAmount, value); }
        }

        public int UpFrontPercent
        {
            get { return this.upFrontPercent; }
            set { this.SetProperty(ref this.upFrontPercent, value); }
        }

        public int ExtendedPercent
        {
            get { return this.extendedPercent; }
            set { this.SetProperty(ref this.extendedPercent, value, nameof(this.ExtendedPercent), this.UpdateExtendedAmount); }
        }

        public decimal TimeExtended
        {
            get { return this.timeExtended; }
            set { this.SetProperty(ref this.timeExtended, value, nameof(this.TimeExtended), this.UpdateTimeToHoursAndMinutes); }
        }

        public int TimeExtendedHours
        {
            get { return this.timeExtendedHours; }
            set { this.SetProperty(ref this.timeExtendedHours, value); }
        }

        public int TimeExtendedMinutes
        {
            get { return this.timeExtendedMinutes; }
            set { this.SetProperty(ref this.timeExtendedMinutes, value); }
        }

        private void Refresh(JournalEntryDetails details)
        {
            this.IsBusy = true;

            this.InsulinAmount = details.InsulinAmount;
            this.UpFrontAmount = details.UpFrontAmount;
            this.ExtendedAmount = details.ExtendedAmount;
            this.TimeExtended = details.TimeExtended;
            this.TimeExtendedHours = details.TimeExtendedHours;
            this.TimeExtendedMinutes = details.TimeExtendedMinutes;
            this.UpFrontPercent = details.UpFrontPercent;
            this.ExtendedPercent = details.ExtendedPercent;

            this.IsBusy = false;
        }

        private void UpdateInsulinPercents()
        {
            if (this.IsNotBusy)
            {
                if (this.UpFrontAmount > 0 && this.InsulinAmount > 0)
                {
                    this.UpFrontPercent = (int)Math.Round((this.UpFrontAmount / this.InsulinAmount) * 100);
                    this.ExtendedPercent = 100 - this.UpFrontPercent;
                }
            }
        }

        private void UpdateExtendedAmount()
        {
            if (this.IsNotBusy)
            {
                if (this.UpFrontAmount > 0 && this.InsulinAmount > 0)
                {
                    this.ExtendedAmount = this.InsulinAmount - this.UpFrontAmount;
                }
            }
        }

        private void UpdateTimeToHoursAndMinutes()
        {
            if (this.IsNotBusy)
            {
                this.TimeExtendedHours = (int)Math.Floor(this.TimeExtended);
                this.TimeExtendedMinutes = (int)((this.TimeExtended - TimeExtendedHours) * 60);
            }
        }
    }
}
