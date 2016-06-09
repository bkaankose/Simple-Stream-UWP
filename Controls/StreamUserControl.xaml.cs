using Prism.Windows.Navigation;
using Microsoft.Practices.Unity;
using Simple_Stream_UWP.Models;
using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Simple_Stream_UWP.Controls
{
    public sealed partial class StreamUserControl : UserControl
    {
        private StreamInformation currentContext;
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
            this.Loaded += (c, r) => { currentContext = this.DataContext as StreamInformation; };
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

        private void FavoriteChannel(object sender, RoutedEventArgs e) { currentContext.IsFavorited = true; }
        private void UnfavoriteChannel(object sender, RoutedEventArgs e) { currentContext.IsFavorited = false; }
        private async void OpenInBrowser(object sender, RoutedEventArgs e) { await Launcher.LaunchUriAsync(new Uri($"http://twitch.tv/{currentContext.Channel?.ChannelName}")); }
        private void PlayClicked(object sender, RoutedEventArgs e) { App.Current.Container.Resolve<INavigationService>().Navigate("LiveStream", currentContext.Channel.ChannelName); }
    }
}
