using System;
using KingTides.Core.Api.Models;
using KingTides.Core.ViewModels;

namespace KingTides.Wp8.Pan.SampleData
{
    public class ExampleMainViewModel : MainViewModel
    {
        public ExampleMainViewModel() : base(null, null, null)
        {
            TideEventsVIC.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location", HighTideOccurs = new DateTime(2014, 10, 8, 17, 35, 00)} });
            TideEventsNSW.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location", HighTideOccurs = new DateTime(2014, 10, 8, 17, 35, 00) } });
            TideEventsQLD.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location", HighTideOccurs = new DateTime(2014, 10, 8, 17, 35, 00) } });
            TideEventsSA.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location", HighTideOccurs = new DateTime(2014, 10, 8, 17, 35, 00) } });
            TideEventsWA.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location", HighTideOccurs = new DateTime(2014, 10, 8, 17, 35, 00) } });
            TideEventsTAS.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location", HighTideOccurs = new DateTime(2014, 10, 8, 17, 35, 00) } });
            TideEventsNT.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location", HighTideOccurs = new DateTime(2014, 10, 8, 17, 35, 00) } });
        }
    }
}
