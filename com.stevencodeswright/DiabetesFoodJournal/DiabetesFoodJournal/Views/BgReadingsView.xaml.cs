using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesFoodJournal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BgReadingsView : ContentView
    {

        public BgReadingsView()
        {
            InitializeComponent();
            this.LogAgainIsOpen = false;
        }

        public bool LogAgainIsOpen { get; private set; }

        private void logAgainButton_Clicked(object sender, EventArgs e)
        {
            this.OpenMenu();
        }

        public void OpenMenu()
        {
            this.LogAgainIsOpen = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await this.hiddenGrid.TranslateTo(this.hiddenGrid.TranslationX, this.bgChart.Height).ConfigureAwait(false);
            });
        }

        public void CloseMenu()
        {
            this.LogAgainIsOpen = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await this.hiddenGrid.TranslateTo(this.hiddenGrid.TranslationX, this.readingsView.Height).ConfigureAwait(false);
            });
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var stepValue = 25;
            var tempValue = e.NewValue * 100;
            var newStep = Math.Round(tempValue / stepValue);

            this.adjustInsulinSlider.Value = (newStep * stepValue) / 100;
        }
    }
}