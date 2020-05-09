using System;
using System.Collections;
using System.Collections.Specialized;
using Xamarin.Forms.Internals;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace DiabetesFoodJournal.ResourceDictionaries.ViewTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultItemView : ContentView
    {
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(SearchResultItemView));
        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(SearchResultItemView));
        public static readonly BindableProperty FoodNameProperty = BindableProperty.Create(nameof(FoodName), typeof(string), typeof(SearchResultItemView), string.Empty);
        public static readonly BindableProperty LoggedProperty = BindableProperty.Create(nameof(Logged), typeof(DateTime?), typeof(SearchResultItemView), null);
        public static readonly BindableProperty CarbCountProperty = BindableProperty.Create(nameof(CarbCount), typeof(int), typeof(SearchResultItemView), 0);
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(SearchResultItemView), false, BindingMode.TwoWay);

        public static readonly BindableProperty TagListProperty =
			BindableProperty.Create(nameof(TagList), typeof(IEnumerable), typeof(ItemsView<Cell>), null,
									propertyChanged: OnItemsSourceChanged);

        static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var element = newValue as Element;
            if (element == null)
                return;
            element.Parent = (Element)bindable;
        }

        public SearchResultItemView()
        {
            InitializeComponent();
        }

        public string FoodName
        {
            get => (string)GetValue(FoodNameProperty);
            set => SetValue(FoodNameProperty, value);
        }

        public DateTime? Logged
        {
            get => (DateTime?)GetValue(LoggedProperty);
            set => SetValue(LoggedProperty, value);
        }

        public int CarbCount
        {
            get => (int)GetValue(CarbCountProperty);
            set => SetValue(CarbCountProperty, value);
        }

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public ICommand TappedCommand
        {
            get => (ICommand)GetValue(TappedCommandProperty);
            set => SetValue(TappedCommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public IEnumerable TagList
        {
            get => (IEnumerable)GetValue(TagListProperty);
            set => SetValue(TagListProperty, value);
        }
    }
}