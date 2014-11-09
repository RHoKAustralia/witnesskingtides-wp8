using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingTides.Core.Api.Models;
using KingTides.Core.ViewModels;

namespace KingTides.Wp8.Pan.SampleData
{
    public class ExampleMainViewModel : MainViewModel
    {
        public ExampleMainViewModel() : base(null, null)
        {
            TideEventsVIC.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location" } });
            TideEventsNSW.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location" } });
            TideEventsQLD.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location" } });
            TideEventsSA.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location" } });
            TideEventsWA.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location" } });
            TideEventsTAS.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location" } });
            TideEventsNT.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { Description = "some description", Location = "some location" } });
        }
    }
}
