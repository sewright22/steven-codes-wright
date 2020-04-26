using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DiabetesFoodJournal.Models;
using DiabetesFoodJournal.Views;
using DiabetesFoodJournal.ViewModels;
using SkiaSharp;
using DiabetesFoodJournal.Services;

namespace DiabetesFoodJournal.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class TestChartPage : ContentView
    {
        public TestChartPage()
        {
            InitializeComponent();

            Load();
        }

        private async void Load()
        {
            var dataStore = new MockReadingDataStore();
            var readings = (await dataStore.GetItemsAsync()).ToList();
            var entries = new[]
            {
                new Microcharts.Entry(readings[0].Reading){Color = SKColor.Parse("#266489"), ValueLabel = readings[0].Reading.ToString()},
                new Microcharts.Entry(readings[1].Reading){Color = SKColor.Parse("#266489"), ValueLabel = readings[1].Reading.ToString()},
                new Microcharts.Entry(readings[2].Reading){Color = SKColor.Parse("#266489"), ValueLabel = readings[2].Reading.ToString()},
                new Microcharts.Entry(readings[3].Reading){Color = SKColor.Parse("#266489"), ValueLabel = readings[3].Reading.ToString()},
                new Microcharts.Entry(readings[4].Reading){Color = SKColor.Parse("#266489"), ValueLabel = readings[4].Reading.ToString()},
                new Microcharts.Entry(readings[5].Reading){Color = SKColor.Parse("#266489"), ValueLabel = readings[5].Reading.ToString()},
                new Microcharts.Entry(readings[6].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[6].Reading.ToString()},
                new Microcharts.Entry(readings[7].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[7].Reading.ToString()},
                new Microcharts.Entry(readings[8].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[8].Reading.ToString()},
                new Microcharts.Entry(readings[9].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[9].Reading.ToString()},
                new Microcharts.Entry(readings[10].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[10].Reading.ToString()},
                new Microcharts.Entry(readings[11].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[11].Reading.ToString()},
                new Microcharts.Entry(readings[12].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[12].Reading.ToString()},
                new Microcharts.Entry(readings[13].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[13].Reading.ToString()},
                new Microcharts.Entry(readings[14].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[14].Reading.ToString()},
                new Microcharts.Entry(readings[15].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[15].Reading.ToString()},
                new Microcharts.Entry(readings[16].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[16].Reading.ToString()},
                new Microcharts.Entry(readings[17].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[17].Reading.ToString()},
                new Microcharts.Entry(readings[18].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[18].Reading.ToString()},
                new Microcharts.Entry(readings[19].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[19].Reading.ToString()},
                new Microcharts.Entry(readings[20].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[20].Reading.ToString()},
                new Microcharts.Entry(readings[21].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[21].Reading.ToString()},
                new Microcharts.Entry(readings[22].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[22].Reading.ToString()},
                new Microcharts.Entry(readings[23].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[23].Reading.ToString()},
                new Microcharts.Entry(readings[24].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[24].Reading.ToString()},
                new Microcharts.Entry(readings[25].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[25].Reading.ToString()},
                new Microcharts.Entry(readings[26].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[26].Reading.ToString()},
                new Microcharts.Entry(readings[27].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[27].Reading.ToString()},
                new Microcharts.Entry(readings[28].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[28].Reading.ToString()},
                new Microcharts.Entry(readings[29].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[29].Reading.ToString()},
                new Microcharts.Entry(readings[30].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[30].Reading.ToString()},
                new Microcharts.Entry(readings[31].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[31].Reading.ToString()},
                new Microcharts.Entry(readings[32].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[32].Reading.ToString()},
                new Microcharts.Entry(readings[33].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[33].Reading.ToString()},
                new Microcharts.Entry(readings[34].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[34].Reading.ToString()},
                new Microcharts.Entry(readings[35].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[35].Reading.ToString()},
                new Microcharts.Entry(readings[36].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[36].Reading.ToString()},
                new Microcharts.Entry(readings[37].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[37].Reading.ToString()},
                new Microcharts.Entry(readings[38].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[38].Reading.ToString()},
                new Microcharts.Entry(readings[39].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[39].Reading.ToString()},
                new Microcharts.Entry(readings[40].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[40].Reading.ToString()},
                new Microcharts.Entry(readings[41].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[41].Reading.ToString()},
                new Microcharts.Entry(readings[42].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[42].Reading.ToString()},
                new Microcharts.Entry(readings[43].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[43].Reading.ToString()},
                new Microcharts.Entry(readings[44].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[44].Reading.ToString()},
                new Microcharts.Entry(readings[45].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[45].Reading.ToString()},
                new Microcharts.Entry(readings[46].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[46].Reading.ToString()},
                new Microcharts.Entry(readings[47].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[47].Reading.ToString()},
                new Microcharts.Entry(readings[48].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[48].Reading.ToString()},
                new Microcharts.Entry(readings[49].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[49].Reading.ToString()},
                new Microcharts.Entry(readings[50].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[50].Reading.ToString()},
                new Microcharts.Entry(readings[51].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[51].Reading.ToString()},
                new Microcharts.Entry(readings[52].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[52].Reading.ToString()},
                new Microcharts.Entry(readings[53].Reading){Color = SKColor.Parse("#68B9C0"), ValueLabel = readings[53].Reading.ToString()},
                new Microcharts.Entry(readings[54].Reading){Color = SKColor.Parse("#90D585"), ValueLabel = readings[54].Reading.ToString()},
                new Microcharts.Entry(readings[55].Reading){Color = SKColor.Parse("#90D585"), ValueLabel = readings[55].Reading.ToString()},
                new Microcharts.Entry(readings[56].Reading){Color = SKColor.Parse("#90D585"), ValueLabel = readings[56].Reading.ToString()},
                new Microcharts.Entry(readings[57].Reading){Color = SKColor.Parse("#90D585"), ValueLabel = readings[57].Reading.ToString()},
                new Microcharts.Entry(readings[58].Reading){Color = SKColor.Parse("#90D585"), ValueLabel = readings[58].Reading.ToString()},
                new Microcharts.Entry(readings[59].Reading){Color = SKColor.Parse("#90D585"), ValueLabel = readings[59].Reading.ToString()}
            };                                 

            this.chart1.Chart = new Microcharts.LineChart() { Entries = entries };
        }
    }
}