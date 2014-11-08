using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using KingTides.Core.Api.Communication;
using KingTides.Core.Api.Models;

namespace KingTides.Core.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            TideEventsNSW = new TideEventsViewModel();
            TideEventsVIC = new TideEventsViewModel();
            TideEventsQLD = new TideEventsViewModel();
            TideEventsNT = new TideEventsViewModel();
            TideEventsSA = new TideEventsViewModel();
            TideEventsWA = new TideEventsViewModel();
            TideEventsTAS = new TideEventsViewModel();
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
        public async void LoadData()
        {
            IsDataLoaded = false;
            var client = new Client("http://10.0.0.19:3000", new WebRequestFactory());
            try
            {
                var data = await client.GetKingTideEventsAsync();
                data = data
                    .OrderByDescending(e => e.Event.HighTideOccurs)
                    .ToArray();
                UpdateEvents(data, TideEventsNSW, "nsw");
                UpdateEvents(data, TideEventsNT, "nt");
                UpdateEvents(data, TideEventsQLD, "qld");
                UpdateEvents(data, TideEventsSA, "sa");
                UpdateEvents(data, TideEventsTAS, "tas");
                UpdateEvents(data, TideEventsVIC, "vic");
                UpdateEvents(data, TideEventsWA, "wa");
            }
            catch (Exception e)
            {
            }
            IsDataLoaded = true;
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