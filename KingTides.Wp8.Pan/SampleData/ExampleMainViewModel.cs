using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingTides.Core.Api.Models;
using KingTides.Wp8.Pan.ViewModels;

namespace KingTides.Wp8.Pan.SampleData
{
    public class ExampleMainViewModel : MainViewModel
    {
        public ExampleMainViewModel()
        {
            TideEventsVIC.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { description = "some description", location = "some location" } });
            TideEventsNSW.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { description = "some description", location = "some location" } });
            TideEventsQLD.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { description = "some description", location = "some location" } });
            TideEventsSA.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { description = "some description", location = "some location" } });
            TideEventsWA.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { description = "some description", location = "some location" } });
            TideEventsTAS.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { description = "some description", location = "some location" } });
            TideEventsNT.KingTideEvents.Add(new KingTideEvent { Event = new TideEvent { description = "some description", location = "some location" } });
        }
    }
}
