using System;
using System.Device.Location;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KingTides.Core.Api.Communication;
using KingTides.Core.Api.Models;
using KingTides.Core.Extensions;
using KingTides.Core.Settings;
using KingTides.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Tasks;

namespace KingTides.Wp8.Pan
{
    public partial class LocationPage : PhoneApplicationPage
    {

        private readonly PhotoChooserTask _photoChooserTask;
        private CameraCaptureTask _cameraCaptureTask;

        private WriteableBitmap _writeable;

        public LocationPage()
        {
            InitializeComponent();
            _photoChooserTask = new PhotoChooserTask();
            _photoChooserTask.Completed += _photoChooserTask_Completed;
            _cameraCaptureTask = new CameraCaptureTask();
            _cameraCaptureTask.Completed += _cameraCaptureTask_Completed;
        }

        void _cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            ExtractPhoto(e);
        }

        void _photoChooserTask_Completed(object sender, PhotoResult e)
        {
            ExtractPhoto(e);
        }

        private void ExtractPhoto(PhotoResult e)
        {
            if (e.TaskResult != TaskResult.OK) return;
            var image = new BitmapImage();
            image.SetSource(e.ChosenPhoto);
            //ViewModel.Picture = image;

            var st = new ScaleTransform
            {
                ScaleX = 0.2,
                ScaleY = 0.2
            };

            _writeable = new WriteableBitmap(
                new Image
                {
                    Width = 800,
                    Height = 600,
                    Visibility = Visibility.Collapsed,
                    Source = image
                }, st);
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

        private void Upload_OnClick(object sender, EventArgs e)
        {
            _photoChooserTask.Show();
        }

        private void Photo_OnClick(object sender, EventArgs e)
        {
            _cameraCaptureTask.Show();
        }
    }
}