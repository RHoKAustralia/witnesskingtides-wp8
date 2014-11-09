using System;
using System.Device.Location;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KingTides.Core.Api.Communication;
using KingTides.Core.Api.Models;
using KingTides.Core.Extensions;
using KingTides.Core.Settings;
using KingTides.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;

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

            string msg;
            if (NavigationContext.QueryString.TryGetValue("data", out msg))
            {
                DataContext = new LocationViewModel(PrivateSettings.Default.Endpoint, new WebRequestFactory()) {TideEvent = msg.FromJson<TideEvent>()};
            }

            ShowLocationOnMap();

            GetNearbyPhotos();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            var location = DataContext as LocationViewModel;
            if (location == null) return;
            location.ContinueLoading = false;
        }

        private void GetNearbyPhotos()
        {
            var location = DataContext as LocationViewModel;
            if (location == null) return;
            location.LoadPhotos();
        }

        private void ShowLocationOnMap()
        {
            var location = DataContext as LocationViewModel;
            if (location == null) return;
            var tideEvent = location.TideEvent;

            var locationCoordinate = new GeoCoordinate(Convert.ToDouble(tideEvent.Latitude), Convert.ToDouble(tideEvent.Longitude));
            MapWithLocation.Center = locationCoordinate;
            MapWithLocation.ZoomLevel = 13;

            var circle = new Ellipse
            {
                Fill = new SolidColorBrush(Colors.Orange),
                Height = 20,
                Width = 20,
                Opacity = 10,
                Stroke = new SolidColorBrush(Colors.Black)
            };
            var overlay = new MapOverlay
            {
                Content = circle,
                PositionOrigin = new Point(0.5, 0.5),
                GeoCoordinate = locationCoordinate
            };

            var layer = new MapLayer {overlay};

            MapWithLocation.Layers.Add(layer);
        }
    }
}