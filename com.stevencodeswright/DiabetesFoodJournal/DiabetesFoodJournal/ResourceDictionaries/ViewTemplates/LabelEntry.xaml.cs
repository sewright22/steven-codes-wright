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
    public partial class LabelEntry : ContentView
    {
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(LabelEntry), string.Empty);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(LabelEntry), string.Empty);
        public static readonly BindableProperty AppendProperty = BindableProperty.Create(nameof(Append), typeof(string), typeof(LabelEntry), string.Empty);
        public static readonly BindableProperty EntryIsVisibleProperty = BindableProperty.Create(nameof(EntryIsVisible), typeof(bool), typeof(LabelEntry), false);
        public static readonly BindableProperty LabelIsVisibleProperty = BindableProperty.Create(nameof(LabelIsVisible), typeof(bool), typeof(LabelEntry), true);


        public LabelEntry()
        {
            InitializeComponent();
        }
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Append
        {
            get => (string)GetValue(AppendProperty);
            set => SetValue(AppendProperty, value);
        }

        public bool EntryIsVisible
        {
            get => (bool)GetValue(EntryIsVisibleProperty);
            set => SetValue(EntryIsVisibleProperty, value);
        }

        public bool LabelIsVisible
        {
            get => (bool)GetValue(LabelIsVisibleProperty);
            set => SetValue(LabelIsVisibleProperty, value);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            EntryIsVisible = true;
            LabelIsVisible = false;
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            EntryIsVisible = false;
            LabelIsVisible = true;
        }
    }
}