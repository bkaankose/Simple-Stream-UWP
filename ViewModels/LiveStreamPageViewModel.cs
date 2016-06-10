using Simple_Stream_UWP.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using Simple_Stream_UWP.Interfaces;
using System.Collections.ObjectModel;
using Prism.Commands;
using Windows.Media.Streaming.Adaptive;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Simple_Stream_UWP.Behaviors;
using System.Diagnostics;

namespace Simple_Stream_UWP.ViewModels
{
    public class LiveStreamPageViewModel : ViewModel
    {
        #region Services & Repositories

        private readonly ITwitchRepository _twitchRepository;

        #endregion

        #region Properties
        private string _navigationParameter = string.Empty; // Game name.

        private AdaptiveMediaSource _mediaSource;

        public AdaptiveMediaSource MediaSource
        {
            get { return _mediaSource; }
            set
            {
                _mediaSource = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands

        #endregion
        public LiveStreamPageViewModel(IDeviceGestureService gestureService, INavigationService navigationService,ITwitchRepository twitchRepository) : base(gestureService, navigationService)
        {
            _navigationService = navigationService;
            _twitchRepository = twitchRepository;
            _gestureService = gestureService;
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            if (e.Parameter != null)
                _navigationParameter = e.Parameter.ToString(); // Get navigated game name parameter.

            // Initialize streaming.
            var streamResult = await _twitchRepository.FetchStreamHLS(_navigationParameter);
            if(streamResult.Status == AdaptiveMediaSourceCreationStatus.Success)
            {
                MediaSource = streamResult.MediaSource;
                foreach (var b in streamResult.MediaSource.AvailableBitrates) // Bitrate info for debugging.
                    Debug.WriteLine(b);
            }
            else
            {
                // TODO : Log exception.
            }
        }
    }
}
