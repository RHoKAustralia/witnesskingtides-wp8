using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace KingTides.Wp8.Pan
{
    public partial class ExtendedSplashScreen : PhoneApplicationPage
    {
        public ExtendedSplashScreen()
        {
            InitializeComponent();

            Splash_Screen();
        }

        async void Splash_Screen()
        {
            await Task.Delay(TimeSpan.FromSeconds(3)); // set your desired delay
            NavigationService.Navigate(new Uri("/MainPage.xaml?removesplash=1", UriKind.Relative)); // call MainPage
        }
    }
}