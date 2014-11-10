using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace KingTides.Wp8.Pan
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string msg;
            if (NavigationContext.QueryString.TryGetValue("removesplash", out msg))
            {
                NavigationService.RemoveBackEntry();
            }

            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
    }
}