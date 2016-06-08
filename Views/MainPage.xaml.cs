using Simple_Stream_UWP.Base;
using Simple_Stream_UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class MainPage : BasePage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            featuredGamesGridView.SizeChanged += GridViewSizeChanged;
            
        }
        private void GridViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var panel = (ItemsWrapGrid)featuredGamesGridView.ItemsPanelRoot;
            if(e.NewSize.Width < 600)
                panel.ItemWidth = panel.ItemHeight = e.NewSize.Width / 2;
            else if(e.NewSize.Width > 600 && e.NewSize.Width < 1200)
                panel.ItemWidth = panel.ItemHeight = e.NewSize.Width / 3;
            else
                panel.ItemWidth = panel.ItemHeight = e.NewSize.Width / 5;
        }

        private void AutoSuggestBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
                (this.DataContext as MainPageViewModel).QueryCommand.Execute();
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            (this.DataContext as MainPageViewModel).QueryCommand.Execute();
        }
    }
}
