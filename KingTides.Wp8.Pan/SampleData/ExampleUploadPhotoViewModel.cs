using System;
using KingTides.Core.Api.Models;
using KingTides.Core.ViewModels;

namespace KingTides.Wp8.Pan.SampleData
{
    public class ExampleUploadPhotoViewModel : UploadPhotoViewModel
    {
        public ExampleUploadPhotoViewModel()
            : base(null, null)
        {
            TideEvent = new TideEvent
            {
                Location = "Somewhere over the rainbow",
                HighTideOccurs = new DateTime(2014, 12, 12, 8, 30, 0),
                EventStart = new DateTime(2014, 12, 11),
                EventEnd = new DateTime(2014, 12, 13)
            };
        }
    }
}