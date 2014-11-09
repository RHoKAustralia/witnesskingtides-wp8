using System.ComponentModel;
using System.Runtime.CompilerServices;
using KingTides.Core.Annotations;
using KingTides.Core.Api.Models;
using KingTides.Core.Extensions;

namespace KingTides.Core.ViewModels
{
    public class LocationPhotoViewModel : INotifyPropertyChanged
    {
        private const int MaxRenderWidth = 300;
        private const int MaxRenderHeight = 300;

        public LocationPhotoViewModel(FlickrPhoto photo)
        {
            Photo = photo;
        }
        public FlickrPhoto Photo { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ImageLocation { get { return Photo.Maybe(_ => _.UrlS); }}
        public int Width { get { return CalculateRenderWidth(); } }
        public int Height { get { return CalculateRenderHeight(); } }

        private int CalculateRenderWidth()
        {
            if (Photo == null) return MaxRenderWidth;
            if (Photo.HeightS > Photo.WidthS)
            {
                return (Photo.WidthS * MaxRenderWidth / Photo.HeightS);
            }
            return MaxRenderWidth;
        }

        private int CalculateRenderHeight()
        {
            if (Photo == null) return MaxRenderHeight;
            if (Photo.WidthS > Photo.HeightS)
            {
                return (Photo.HeightS*MaxRenderHeight/Photo.WidthS);
            }
            return MaxRenderHeight;
        }
    }
}