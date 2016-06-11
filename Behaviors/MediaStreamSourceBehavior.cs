using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Streaming.Adaptive;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Simple_Stream_UWP.Behaviors
{
    public class MediaStreamSourceBehavior : DependencyObject, IBehavior
    {
        private DependencyObject _mediaElement;
        public DependencyObject AssociatedObject
        {
            get
            {
                return _mediaElement;
            }
        }

        public void Attach(DependencyObject associatedObject)
        {
            _mediaElement = associatedObject as MediaElement;
            if (_mediaElement == null)
                throw new ArgumentException("MediaStreamSourceBehavior can be attached only to MediaElement.");

            _mediaElement = associatedObject;
        }

        public void Detach()
        {
            _mediaElement = null;
        }

        public AdaptiveMediaSource Media
        {
            get { return (AdaptiveMediaSource)GetValue(MediaProperty); }
            set { SetValue(MediaProperty, value); }
        }

        public static readonly DependencyProperty MediaProperty = DependencyProperty.Register("Media", typeof(AdaptiveMediaSource), typeof(MediaStreamSourceBehavior), new PropertyMetadata(null, OnMediaChanged));

        private static void OnMediaChanged(object sender,
        DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as MediaStreamSourceBehavior;
            if (behavior.AssociatedObject == null)
                return;

            var mediaElement = behavior.AssociatedObject as MediaElement;
            if (mediaElement != null)
            {
                if(e.NewValue != null)
                    mediaElement.SetMediaStreamSource((AdaptiveMediaSource)e.NewValue);
                else
                {
                    // Dispose media element.
                    mediaElement.Source = null;
                    mediaElement.Stop();
                    mediaElement = null;
                }
            }
        }
    }
}
