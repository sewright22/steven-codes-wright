using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesFoodJournal.ViewCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JournalEntryViewCell : ViewCell
    {

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(JournalEntryViewCell));
        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(JournalEntryViewCell));
        public static readonly BindableProperty FoodNameProperty = BindableProperty.Create(nameof(FoodName), typeof(string), typeof(JournalEntryViewCell), string.Empty); 
        
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
        public JournalEntryViewCell()
        {
            InitializeComponent();
        }

        public string FoodName
        {
            get => (string)GetValue(FoodNameProperty);
            set => SetValue(FoodNameProperty, value);
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