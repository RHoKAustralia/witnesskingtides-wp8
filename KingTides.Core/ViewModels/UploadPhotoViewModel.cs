using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KingTides.Core.Annotations;
using KingTides.Core.Api.Communication;
using KingTides.Core.Api.Models;
using KingTides.Core.Extensions;
using KingTides.Core.Settings;

namespace KingTides.Core.ViewModels
{
    public class UploadPhotoViewModel : INotifyPropertyChanged
    {
        private readonly string _endpoint;
        private readonly IWebRequestFactory _factory;
        private bool _useCurrentLocation;
        private string _description;
        private bool _isLoading;

        public UserDetails UserDetails { get; private set; }

        public UploadPhotoViewModel(string endpoint, IWebRequestFactory factory, UserDetails userDetails = null)
        {
            _endpoint = endpoint;
            _factory = factory;
            UserDetails = userDetails ?? new UserDetails();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TideEvent TideEvent { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (value.Equals(_isLoading)) return;
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool UseCurrentLocation
        {
            get { return _useCurrentLocation; }
            set
            {
                if (value.Equals(_useCurrentLocation)) return;
                _useCurrentLocation = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return UserDetails.FirstName; }
            set
            {
                if (value == UserDetails.FirstName) return;
                UserDetails.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return UserDetails.LastName; }
            set
            {
                if (value == UserDetails.LastName) return;
                UserDetails.LastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return UserDetails.Email; }
            set
            {
                if (value == UserDetails.Email) return;
                UserDetails.Email = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public async Task<string> UploadAsync(string imageData)
        {
            var client = new Client(_endpoint, _factory);
            var uploadPhoto = new UploadPhoto
            {
                Photo = imageData,
                FirstName = FirstName.NullIfEmptyOrWhiteSpace() ?? "Not Supplied",
                LastName = LastName.NullIfEmptyOrWhiteSpace() ?? "Not Supplied",
                Description = Description.NullIfEmptyOrWhiteSpace() ?? "Not Supplied",
                Email = Email.NullIfEmptyOrWhiteSpace() ?? "Not Supplied",
                FileId = "1",
                Latitude = UseCurrentLocation ? Latitude ?? TideEvent.Latitude : TideEvent.Latitude,
                Longitude = UseCurrentLocation ? Longitude ?? TideEvent.Longitude : TideEvent.Longitude,
            };

            try
            {
                var data = await client.UploadPhotoAsync(uploadPhoto);
                var id = data.PhotoId;
                return id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
