using DiabetesFoodJournal.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;
using Xamarin.Forms;

namespace DiabetesFoodJournal.ViewModels.JournalEntryUpdate
{
    public class JournalEntryUpdateViewModel : BaseViewModel
    {
        private decimal insulinAmount;
        private decimal upFrontAmount;
        private decimal extendedAmount;
        private decimal timeExtended;
        private int upFrontPercent;
        private int extendedPercent;
        private int timeExtendedHours;
        private int timeExtendedMinutes;
        private int carbCount;
        private decimal carbRatio;
        private bool isExtended;
        private readonly IMessagingCenter messagingCenter;
        private readonly IJournalEntryDetailsService journalEntryDetailsService;

        public JournalEntryUpdateViewModel(IMessagingCenter messagingCenter, IJournalEntryDetailsService journalEntryDetailsService)
        {
            this.messagingCenter = messagingCenter ?? throw new ArgumentNullException(nameof(messagingCenter));
            this.journalEntryDetailsService = journalEntryDetailsService ?? throw new ArgumentNullException(nameof(journalEntryDetailsService));
            this.carbRatio = (decimal)7.5;
            this.LogCommand = new AsyncCommand(this.LogNewEntry);
            this.messagingCenter.Subscribe<JournalEntrySummary>(this, "LogNew", this.CreateNewJournalEntry);
            this.messagingCenter.Subscribe<JournalEntrySummary>(this, "EditExisting", async entry => await this.Refresh(entry).ConfigureAwait(false));
        }

        public int CarbCount
        {
            get { return this.carbCount; }
            set { this.SetProperty(ref this.carbCount, value, nameof(this.CarbCount), this.UpdateInsulinAmount); }
        }

        public decimal InsulinAmount
        {
            get { return this.insulinAmount; }
            set { this.SetProperty(ref this.insulinAmount, value, nameof(this.InsulinAmount), this.UpdateInsulinPercents); }
        }

        public decimal CarbRatio
        {
            get { return this.carbRatio; }
            set { this.SetProperty(ref this.carbRatio, value); }
        }

        public decimal UpFrontAmount
        {
            get { return this.upFrontAmount; }
            set { this.SetProperty(ref this.upFrontAmount, value); }
        }

        public decimal ExtendedAmount
        {
            get { return this.extendedAmount; }
            set { this.SetProperty(ref this.extendedAmount, value); }
        }

        public bool IsExtended
        {
            get { return this.isExtended; }
            set { this.SetProperty(ref this.isExtended, value, nameof(this.IsExtended), this.UpdateInsulinPercents); }
        }

        public int UpFrontPercent
        {
            get { return this.upFrontPercent; }
            set { this.SetProperty(ref this.upFrontPercent, value, nameof(this.UpFrontPercent), this.UpdateExtendedPercent); }
        }

        public int ExtendedPercent
        {
            get { return this.extendedPercent; }
            set { this.SetProperty(ref this.extendedPercent, value, nameof(this.ExtendedPercent), this.UpdateUpFrontPercent); }
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

        public AsyncCommand LogCommand { get; }

        private  Task<JournalEntryDetails> Model { get; set; }

        private void CreateNewJournalEntry(JournalEntrySummary obj)
        {
            throw new NotImplementedException();
        }

        private async Task Refresh(JournalEntrySummary obj)
        {
            this.IsBusy = true;
            var details = await this.journalEntryDetailsService.GetDetails(obj.ID);

            this.Title = details.Title;
            this.CarbCount = details.CarbCount.HasValue ? details.CarbCount.Value > 100 ? 100 : details.CarbCount.Value : 0;
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

        private Task LogNewEntry()
        {
            throw new NotImplementedException();
        }

        private void UpdateInsulinPercents()
        {
            if (this.IsNotBusy)
            {
                if (this.UpFrontAmount > 0 && this.InsulinAmount > 0)
                {
                    if (this.IsExtended)
                    {
                        this.UpFrontPercent = (int)Math.Round((this.UpFrontAmount / this.InsulinAmount) * 100);
                        this.ExtendedPercent = 100 - this.UpFrontPercent;
                    }
                    else
                    {
                        this.UpFrontPercent = 100;
                        this.ExtendedPercent = 0;
                    }
                }
            }
        }

        private void UpdateInsulinAmount()
        {
            if (this.IsNotBusy)
            {
                this.InsulinAmount = Convert.ToDecimal(this.CarbCount / 7.5);
            }
        }

        private void UpdateExtendedPercent()
        {
            if (this.IsNotBusy)
            {
                this.ExtendedPercent = 100 - this.upFrontPercent;
            }
        }

        private void UpdateUpFrontPercent()
        {
            if (this.IsNotBusy)
            {
                this.UpFrontPercent = 100 - this.extendedPercent;
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
