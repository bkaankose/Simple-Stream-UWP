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
using Windows.UI.ViewManagement;
using Simple_Stream_UWP.Models;
using Windows.Graphics.Display;
using Newtonsoft.Json;
using Windows.UI.Core;

namespace Simple_Stream_UWP.ViewModels
{
    public class LiveStreamPageViewModel : ViewModel
    {
        #region Services & Repositories

        private readonly ITwitchRepository _twitchRepository;

        #endregion

        #region Properties

        private ObservableCollection<StreamInformation> _streamFlow;

        public ObservableCollection<StreamInformation> StreamFlow
        {
            get { return _streamFlow; }
            set { _streamFlow = value; OnPropertyChanged(); }
        }


        private StreamInformation _currentStream;

        public StreamInformation CurrentStream
        {
            get { return _currentStream; }
            set { _currentStream = value; OnPropertyChanged(); }
        }

        private bool _isFullScreen;

        public bool IsFullScreen
        {
            get { return _isFullScreen; }
            set { _isFullScreen = value; OnPropertyChanged(); }
        }

        private bool _isMediaPlaying;

        public bool IsMediaPlaying
        {
            get { return _isMediaPlaying; }
            set { _isMediaPlaying = value;  OnPropertyChanged(); }
        }


        private bool _isStretched;

        public bool IsStretched
        {
            get { return _isStretched; }
            set { _isStretched = value; OnPropertyChanged(); }
        }


        private bool _isBarsOpen;

        public bool IsBarsOpen
        {
            get { return _isBarsOpen; }
            set { _isBarsOpen = value; OnPropertyChanged(); }
        }


        private ObservableCollection<BitrateModel> _availableBitrates;

        public ObservableCollection<BitrateModel> AvailableBitrates
        {
            get { return _availableBitrates; }
            set { _availableBitrates = value;  }
        }

        private BitrateModel _selectedBitrate;

        public BitrateModel SelectedBitrate
        {
            get { return _selectedBitrate; }
            set { _selectedBitrate = value; OnPropertyChanged(); }
        }


        private AdaptiveMediaSource _mediaSource;

        public AdaptiveMediaSource MediaSource
        {
            get { return _mediaSource; }
            set
            {
                _mediaSource = value;
            }
        }

        private string[] BitrateNames = new string[4] { "Low", "Medium", "High", "Source" };

        #endregion

        #region Commands

        public DelegateCommand PlayOrPauseCommand { get; set; }

        #endregion

        public LiveStreamPageViewModel(IDeviceGestureService gestureService, INavigationService navigationService,ITwitchRepository twitchRepository, ISessionStateService sessionStateService) : base(gestureService, navigationService, sessionStateService)
        {
            _twitchRepository = twitchRepository;
            AvailableBitrates = new ObservableCollection<BitrateModel>();
            PlayOrPauseCommand = new DelegateCommand(PlayOrPause);
        }

        private void PlayOrPause()
        {
            if (IsMediaPlaying)
                OnPropertyChanged("StopMedia");
            else
                OnPropertyChanged("PlayMedia");

            IsMediaPlaying = !IsMediaPlaying;
        }

        public async Task SwipeChannel(bool toRight)
        {
            var currentChannelIndex = StreamFlow.IndexOf(StreamFlow.FirstOrDefault(a => a.Channel.ChannelId.Equals(CurrentStream.Channel.ChannelId)));
            bool changed = false;
            if(toRight && StreamFlow.Count - 1 != currentChannelIndex)
            { // Get next channel from right.
                CurrentStream = StreamFlow[currentChannelIndex + 1];
                changed = true;
            }else if(!toRight && currentChannelIndex != 0)
            { // Get previous channel from left.
                CurrentStream = StreamFlow[currentChannelIndex - 1];
                changed = true;
            }

            if(CurrentStream != null && changed)
                await InitializeStreamByChannelName(CurrentStream.Channel.ChannelName);
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            StreamFlow = _twitchRepository.GetLatestLoadedGames();

            if (e.Parameter != null)
                CurrentStream = JsonConvert.DeserializeObject<StreamInformation>(e.Parameter.ToString()); // Get navigated stream object parameter.

            // Try to fullscreen.
            IsFullScreen = ApplicationView.GetForCurrentView().TryEnterFullScreenMode();

            // Initialize stream in the background thread.
            this.PropertyChanged += PropChanged;
            await InitializeStreamByChannelName(CurrentStream.Channel.ChannelName);
        }

       
        private async Task InitializeStreamByChannelName(string channelName)
        {
            IsBarsOpen = false;
            if(MediaSource != null)
            {
                MediaSource = null;
                OnPropertyChanged("StopMedia");
            }

            var streamResult = await _twitchRepository.FetchStreamHLS(channelName);
            if(streamResult == null)
            {
                App.Current.Exit();
                return;
            }
            if (streamResult.Status == AdaptiveMediaSourceCreationStatus.Success)
            {
                MediaSource = streamResult.MediaSource;
                IsMediaPlaying = true;

                if (AvailableBitrates != null)
                    AvailableBitrates.Clear();
                else
                    AvailableBitrates = new ObservableCollection<BitrateModel>();

                for(int i = 0;i < MediaSource.AvailableBitrates.Count - 1;i++) // Load bitrates
                    AvailableBitrates.Add(new BitrateModel() { OriginalBitrate = MediaSource.AvailableBitrates[i], BitrateName = BitrateNames[i] });

                OnPropertyChanged("MediaSource");
                OnPropertyChanged("AvailableBitrates");
            }
            else
            {
                // TODO : Log exception.
            }
        }

        private void PropChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsFullScreen))
            {
                if (!IsFullScreen)
                    ApplicationView.GetForCurrentView().ExitFullScreenMode();
                else
                    ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
            }else if(e.PropertyName == nameof(AvailableBitrates) && AvailableBitrates != null)
            {
                SelectedBitrate = AvailableBitrates[AvailableBitrates.Count / 2]; // Pick mid level bitrate by default.
            }else if(e.PropertyName == nameof(SelectedBitrate) && SelectedBitrate != null)
            {
                if(SelectedBitrate.OriginalBitrate > MediaSource.DesiredMinBitrate)
                {
                    MediaSource.DesiredMaxBitrate = SelectedBitrate.OriginalBitrate;
                    MediaSource.DesiredMinBitrate = SelectedBitrate.OriginalBitrate;
                }else
                {

                    MediaSource.DesiredMinBitrate = SelectedBitrate.OriginalBitrate;
                    MediaSource.DesiredMaxBitrate = SelectedBitrate.OriginalBitrate;
                }
            }
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.None; // Force it to landscape mode.
            MediaSource = null;
            OnPropertyChanged("MediaSource");
        }
    }
}
