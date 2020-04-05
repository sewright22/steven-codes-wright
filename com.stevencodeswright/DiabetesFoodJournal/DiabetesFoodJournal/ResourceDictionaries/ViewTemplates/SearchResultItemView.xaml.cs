using System;
using System.Collections;
using System.Collections.Specialized;
using Xamarin.Forms.Internals;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesFoodJournal.ResourceDictionaries.ViewTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultItemView : ContentView
    {
        public static readonly BindableProperty FoodNameProperty = BindableProperty.Create(nameof(FoodName), typeof(string), typeof(SearchResultItemView), string.Empty);

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

        public IEnumerable TagList
        {
            get => (IEnumerable)GetValue(TagListProperty);
            set => SetValue(TagListProperty, value);
        }
    }
}