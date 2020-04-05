using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesFoodJournal.ResourceDictionaries.ViewTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TagView : ContentView
    {
        public static readonly BindableProperty TagTextProperty = BindableProperty.Create(nameof(TagText), typeof(string), typeof(TagView), string.Empty);
        public TagView()
        {
            InitializeComponent();
        }

        public string TagText
        {
            get => (string)GetValue(TagTextProperty);
            set => SetValue(TagTextProperty, value);
        }
    }
}