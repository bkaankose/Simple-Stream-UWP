using Prism.Windows.Navigation;
using Microsoft.Practices.Unity;
using Simple_Stream_UWP.Models;
using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using Simple_Stream_UWP.Interfaces;

namespace Simple_Stream_UWP.Controls
{
    public sealed partial class StreamUserControl : UserControl
    {
        public bool IsOptionsDialogVisible
        {
            get { return (bool)GetValue(IsOptionsDialogVisibleProperty); }
            set { SetValue(IsOptionsDialogVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsOptionsDialogVisibleProperty =
            DependencyProperty.Register("IsOptionsDialogVisible", typeof(bool), typeof(StreamUserControl),new PropertyMetadata(true,new PropertyChangedCallback(VisibilityChanged)));


        public StreamUserControl()
        {
            this.InitializeComponent();
        }

        private void ThumbnailLoaded(object sender, RoutedEventArgs e)
        {
            imageProgress.IsActive = false;
            imageProgress.Visibility = Visibility.Collapsed;
        }

        private void ThumbnailFailed(object sender, ExceptionRoutedEventArgs e)
        {
            imageProgress.IsActive = false;
            imageProgress.Visibility = Visibility.Collapsed;
            imageFailedMessagePanel.Visibility = Visibility.Visible;
        }

        static void VisibilityChanged(DependencyObject obj,DependencyPropertyChangedEventArgs args)
        {
            var control = obj as StreamUserControl;

            if ((bool)args.NewValue == true)
                control.OptionsLoadingAnimaton.Begin();
            else
                control.OptionsDisappearingAnimaton.Begin();
        }

        private void FavoriteChannel(object sender, RoutedEventArgs e) { (this.DataContext as StreamInformation).IsFavorited = true; }
        private void UnfavoriteChannel(object sender, RoutedEventArgs e) { (this.DataContext as StreamInformation).IsFavorited = false; }
        private async void OpenInBrowser(object sender, RoutedEventArgs e) { await Launcher.LaunchUriAsync(new Uri($"http://twitch.tv/{(this.DataContext as StreamInformation).Channel?.ChannelName}")); }
        private async void PlayClicked(object sender, RoutedEventArgs e)
        {
            if((this.DataContext as StreamInformation).Channel.IsAgeRestricted.GetValueOrDefault())
            {
                var result = await App.Current.Container.Resolve<IPageDialogService>().DisplayConfirmationDialogAsync("This channel may contain violent content. Do you comfirm that you're above 18?","Warning!");
                if (!result)
                    return;
            }
            App.Current.Container.Resolve<INavigationService>().Navigate("LiveStream", JsonConvert.SerializeObject((this.DataContext as StreamInformation)));
        }
    }
}
