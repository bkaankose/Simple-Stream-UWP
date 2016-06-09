using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

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
    }
}
