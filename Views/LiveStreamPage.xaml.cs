using Simple_Stream_UWP.Base;
using Simple_Stream_UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Simple_Stream_UWP.Views
{
    public sealed partial class LiveStreamPage : BasePage
    {
        private LiveStreamPageViewModel ViewModel;
        public LiveStreamPage()
        {
            this.InitializeComponent();
            ViewModel = this.DataContext as LiveStreamPageViewModel;
            ViewModel.PropertyChanged += PropChanged;
        }

        private void PropChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedBitrate")
                bitrateFlyout.Hide();
            else if (e.PropertyName == "PlayMedia")
                mediaElement.Play();
            else if (e.PropertyName == "StopMedia")
                mediaElement.Stop();
            else if(e.PropertyName == "IsBarsOpen")
            {
                if (!ViewModel.IsBarsOpen)
                    openBarsStoryBoard.Begin();
                else
                    hideBarsStoryBoard.Begin();
            }
        }

        private void ScreenTapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.IsBarsOpen = !ViewModel.IsBarsOpen;
        }
    }
}
