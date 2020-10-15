using DiabetesFoodJournal.Services;
using GalaSoft.MvvmLight.Ioc;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeOneFoodJournal.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesFoodJournal.Views.Tag
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTagView : ContentView
    {
        private ITagService tagService;

        public AddTagView()
        {
            InitializeComponent();
            this.tagService = SimpleIoc.Default.GetInstance<ITagService>();
        }

        public async void LoadTags()
        {
            this.autocomplete.DataSource = new ObservableRangeCollection<TagModel>();
            ((ObservableRangeCollection<TagModel>)this.autocomplete.DataSource).ReplaceRange(await this.tagService.GetTags().ConfigureAwait(true));
        }
    }
}