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
        }
    }
}