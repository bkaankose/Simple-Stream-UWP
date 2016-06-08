using Simple_Stream_UWP.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Simple_Stream_UWP.Views
{
    public sealed partial class GameDetailPage : BasePage
    {
        public GameDetailPage()
        {
            this.InitializeComponent();
            streamsGridView.SizeChanged += GridSizeChanged;
        }

        private void GridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var panel = (ItemsWrapGrid)streamsGridView.ItemsPanelRoot;
            if (e.NewSize.Width < 600)
                panel.ItemWidth =  e.NewSize.Width / 1;
            else if (e.NewSize.Width > 600 && e.NewSize.Width < 1200)
                panel.ItemWidth =  e.NewSize.Width / 2;
            else
                panel.ItemWidth =  e.NewSize.Width / 3;
        }
    }
}
