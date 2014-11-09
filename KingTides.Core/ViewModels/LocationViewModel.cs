using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using KingTides.Core.Annotations;
using KingTides.Core.Api.Communication;
using KingTides.Core.Api.Models;

namespace KingTides.Core.ViewModels
{
    public class LocationViewModel : INotifyPropertyChanged
    {
        private readonly string _endpoint;
        private readonly IWebRequestFactory _factory;

        public LocationViewModel(string endpoint, IWebRequestFactory factory)
        {
            _endpoint = endpoint;
            _factory = factory;
            Photos = new ObservableCollection<FlickrPhoto>();
        }

        public TideEvent TideEvent { get; set; }

        public ObservableCollection<FlickrPhoto> Photos { get; private set; }

        public bool ContinueLoading { get; set; }

        public async void LoadPhotos()
        {
            ContinueLoading = true;
            var client = new Client(_endpoint, _factory);
            Photos.Clear();
            try
            {
                var page = 1;
                while (Photos.Count < 20 && ContinueLoading)
                {
                    var photos = await client.GetPhotosForRangeAsync(per_page: 100, page: page);
                    if (!photos.Photos.Photo.Any()) return;
                    foreach (var photo in photos.Photos.Photo)
                    {
                        if (DistanceTo(Convert.ToDouble(photo.Latitude), Convert.ToDouble(photo.Longitude), Convert.ToDouble(TideEvent.Latitude), Convert.ToDouble(TideEvent.Longitude)) < 20)
                            Photos.Add(photo);
                    }
                    page++;
                }
            }
            catch (Exception)
            {
            }
        }

        static public double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double rlon1 = Math.PI * lon1 / 180;
            double rlon2 = Math.PI * lon2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
