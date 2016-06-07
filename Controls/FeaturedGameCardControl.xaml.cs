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

namespace Simple_Stream_UWP.Controls
{
    public sealed partial class FeaturedGameCardControl : UserControl
    {
        public FeaturedGameCardControl()
        {
            this.Width = ConfigurationContext.DEFAULT_MAX_LOGO_WIDTH;
            this.Height = ConfigurationContext.DEFAULT_MAX_LOGO_HEIGHT;
            this.InitializeComponent();
        }

        private void CoverPhotoOpened(object sender, RoutedEventArgs e)
        {
            imageLoadingProgress.IsActive = false;
        }

        private void CoverPhotoFailed(object sender, ExceptionRoutedEventArgs e)
        {
            imageLoadingProgress.IsActive = false;
            imageFailedMessagePanel.Visibility = Visibility.Visible;
        }
    }
}
