using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using KingTides.Core.Api.Models;
using KingTides.Core.Extensions;
using KingTides.Core.Settings;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace KingTides.Wp8.Pan
{
    public partial class LocationPage : PhoneApplicationPage
    {
        public LocationPage()
        {
            InitializeComponent();
        }

        private void MapWithLocation_OnLoaded(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = PrivateSettings.Default.MapApplicationId;
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = PrivateSettings.Default.MapAuthenticationToken;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string msg = "";

            if (NavigationContext.QueryString.TryGetValue("data", out msg))
            {
                this.DataContext = msg.FromJson<TideEvent>();
            }
        }
    }
}