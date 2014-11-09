using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using KingTides.Core.Api.Models;
using KingTides.Core.Extensions;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace KingTides.Wp8.Pan.UserControls
{
    public partial class TideEventUserControl : UserControl
    {
        public TideEventUserControl()
        {
            InitializeComponent();
        }

        private void GestureListener_OnTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var item = sender as StackPanel;
            if (item == null) return;
            var context = item.DataContext as TideEvent;
            if (context == null) return;
            (Application.Current.RootVisual as PhoneApplicationFrame)
                .Navigate(new Uri(string.Format("/LocationPage.xaml?data={0}", context.ToJson()), UriKind.Relative));
        }
    }
}
