using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using KingTides.Core.Annotations;
using KingTides.Core.Api.Models;

namespace KingTides.Core.ViewModels
{
    public class TideEventsViewModel : INotifyPropertyChanged
    {
        public TideEventsViewModel()
        {
            KingTideEvents = new ObservableCollection<KingTideEvent>();
        }

        public ObservableCollection<KingTideEvent> KingTideEvents { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
