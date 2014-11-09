using System;
using KingTides.Core.Api.Models;
using KingTides.Core.ViewModels;

namespace KingTides.Wp8.Pan.SampleData
{
    public class ExampleLocationViewModel : LocationViewModel
    {
        public ExampleLocationViewModel() : base(null, null)
        {
            TideEvent = new TideEvent
            {
                Location = "Somewhere over the rainbow",
                HighTideOccurs = new DateTime(2014, 12, 12, 8, 30, 0),
                EventStart = new DateTime(2014, 12, 11),
                EventEnd = new DateTime(2014, 12, 13)
            };
            Photos.Add(
                new LocationPhotoViewModel(new FlickrPhoto
                {
                    UrlS = "http://www.esa.act.gov.au/wp-content/uploads/lightning-storm-400x300.jpg",
                    HeightS = 150,
                    WidthS = 200
                }));
        }
    }
}