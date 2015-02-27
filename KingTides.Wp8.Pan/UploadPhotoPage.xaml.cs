using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Windows.Devices.Geolocation;
using KingTides.Core.Api.Communication;
using KingTides.Core.Api.Models;
using KingTides.Core.Extensions;
using KingTides.Core.Settings;
using KingTides.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace KingTides.Wp8.Pan
{
    public partial class UploadPhotoPage : PhoneApplicationPage
    {
        private WriteableBitmap _writeable;
        private UploadPhotoViewModel _uploadData;

        public UploadPhotoPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!PhoneApplicationService.Current.State.ContainsKey("photo"))
            {
                this.NavigationService.GoBack();
                return;
            }

            var image = (BitmapImage)PhoneApplicationService.Current.State["photo"];
            PhoneApplicationService.Current.State.Remove("photo");

            var st = new ScaleTransform
            {
                ScaleX = 0.2,
                ScaleY = 0.2
            };

            _writeable = new WriteableBitmap(
                new Image
                {
                    Width = image.PixelWidth,
                    Height = image.PixelHeight,
                    Visibility = Visibility.Collapsed,
                    Source = image
                }, st);

            UserDetails userDetails = null;
            if (IsolatedStorageSettings.ApplicationSettings.Contains("UserDetails"))
            {
                userDetails = (UserDetails)IsolatedStorageSettings.ApplicationSettings["UserDetails"];
            }

            string msg;
            if (NavigationContext.QueryString.TryGetValue("data", out msg))
            {
                _uploadData = new UploadPhotoViewModel(PrivateSettings.Default.Endpoint, new WebRequestFactory(), userDetails)
                {
                    TideEvent = msg.FromJson<TideEvent>()
                };
                DataContext = _uploadData;
            }

            PhotoImage.Source = _writeable;

            var geolocator = new Geolocator();

            try
            {
                var geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                _uploadData.Latitude = (decimal)geoposition.Coordinate.Latitude;
                _uploadData.Longitude = (decimal)geoposition.Coordinate.Longitude;
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                }
                //else
                {
                    // something else happened acquring the location
                }
            }

        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings["UserDetails"] = _uploadData.UserDetails ?? new UserDetails();
            base.OnNavigatingFrom(e);
        }

        // may need a setting screen to allow user to change their mind
        private static bool GetLocationConsent()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                // User has opted in or out of Location
                return (bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"];
            }

            var result = MessageBox.Show("This app accesses your phone's location. Is that ok?",
                "Location", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
            }

            IsolatedStorageSettings.ApplicationSettings.Save();
            return false;
        }

        private async void Send_OnClick(object sender, EventArgs e)
        {
            var upload = DataContext as UploadPhotoViewModel;
            if (upload == null) return;
            if (upload.IsLoading) return;
            upload.IsLoading = true;
            using (var stream = new MemoryStream())
            {
                _writeable.SaveJpeg(stream, _writeable.PixelWidth, _writeable.PixelHeight, 0, 100);
                var binaryData = stream.ToArray();
                var base64String = Convert.ToBase64String(binaryData, 0, binaryData.Length);
                var ids = await Task.WhenAll(upload.UploadAsync(base64String), Sleep(3000));
                //var ids = await Task.WhenAll(Sleep(3000));
                upload.IsLoading = false;
                if (!string.IsNullOrWhiteSpace(ids[0]))
                {
                    MessageBox.Show("Photo has been uploaded!");
                    this.NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Sorry, photo upload failed.");
                }
            }
        }

        private async static Task<string> Sleep(int ms)
        {
            await Task.Delay(ms);
            return null;
        }
    }
}