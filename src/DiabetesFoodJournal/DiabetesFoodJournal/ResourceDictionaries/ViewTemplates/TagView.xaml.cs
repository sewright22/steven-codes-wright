using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesFoodJournal.ResourceDictionaries.ViewTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TagView : ContentView
    {
        public static readonly BindableProperty CanDeleteProperty = BindableProperty.Create(nameof(CanDelete), typeof(bool), typeof(TagView), false, BindingMode.TwoWay, null, CanDeleteChanged);
        public static readonly BindableProperty TagTextProperty = BindableProperty.Create(nameof(TagText), typeof(string), typeof(TagView), string.Empty);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(TagView));
        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(TagView));
        public static readonly BindableProperty DeleteTappedCommandProperty = BindableProperty.Create(nameof(DeleteTappedCommand), typeof(ICommand), typeof(TagView));
        public TagView()
        {
            InitializeComponent();
        }

        public string TagText
        {
            get => (string)GetValue(TagTextProperty);
            set => SetValue(TagTextProperty, value);
        }

        public bool CanDelete
        {
            get => (bool)GetValue(CanDeleteProperty);
            set => SetValue(CanDeleteProperty, value);
        }

        public ICommand TappedCommand
        {
            get => (ICommand)GetValue(TappedCommandProperty);
            set => SetValue(TappedCommandProperty, value);
        }

        public ICommand DeleteTappedCommand
        {
            get => (ICommand)GetValue(DeleteTappedCommandProperty);
            set => SetValue(DeleteTappedCommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        static async void CanDeleteChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                var newBool = (bool)newValue;
                var tag = (TagView)bindable;

                if (newBool)
                {
                    await tag.ScaleTo(1.25).ConfigureAwait(true);
                }
                else
                {
                    await tag.ScaleTo(1.00).ConfigureAwait(true);
                }
            }
        }
    }
}