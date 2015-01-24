using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using KingTides.Core.Api.Communication;
using KingTides.Core.Api.Models;

namespace KingTides.Core.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly string _endpoint;
        private readonly IWebRequestFactory _factory;

        public MainViewModel(string endpoint, IWebRequestFactory factory, KingTideEvent[] events)
        {
            _endpoint = endpoint;
            _factory = factory;
            TideEventsNSW = new TideEventsViewModel();
            TideEventsVIC = new TideEventsViewModel();
            TideEventsQLD = new TideEventsViewModel();
            TideEventsNT = new TideEventsViewModel();
            TideEventsSA = new TideEventsViewModel();
            TideEventsWA = new TideEventsViewModel();
            TideEventsTAS = new TideEventsViewModel();

            UpdateEvents(events);
        }

        public TideEventsViewModel TideEventsNSW { get; private set; }
        public TideEventsViewModel TideEventsVIC { get; private set; }
        public TideEventsViewModel TideEventsQLD { get; private set; }
        public TideEventsViewModel TideEventsNT { get; private set; }
        public TideEventsViewModel TideEventsSA { get; private set; }
        public TideEventsViewModel TideEventsWA { get; private set; }
        public TideEventsViewModel TideEventsTAS { get; private set; }
        
        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async Task<KingTideEvent[]> LoadData()
        {
            IsDataLoaded = false;
            var client = new Client(_endpoint, _factory);
            KingTideEvent[] data = null;
            try
            {
                data = await client.GetKingTideEventsAsync();
                data = data
                    .OrderByDescending(e => e.Event.HighTideOccurs)
                    .ToArray();
                UpdateEvents(data);
            }
            catch (Exception)
            {
            }
            IsDataLoaded = true;
            return data;
        }

        private void UpdateEvents(KingTideEvent[] data)
        {
            if (data == null) return;
            UpdateEvents(data, TideEventsNSW, "nsw");
            UpdateEvents(data, TideEventsNT, "nt");
            UpdateEvents(data, TideEventsQLD, "qld");
            UpdateEvents(data, TideEventsSA, "sa");
            UpdateEvents(data, TideEventsTAS, "tas");
            UpdateEvents(data, TideEventsVIC, "vic");
            UpdateEvents(data, TideEventsWA, "wa");
        }

        private static void UpdateEvents(IEnumerable<KingTideEvent> data, TideEventsViewModel model, string state)
        {
            model.KingTideEvents.Clear();
            foreach (var kingTideEvent in data.Where(e => e.Event.State.ToLowerInvariant() == state))
                model.KingTideEvents.Add(kingTideEvent);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}