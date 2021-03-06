﻿using Simple_Stream_UWP.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using Simple_Stream_UWP.Interfaces;
using Prism.Commands;
using System.Collections.ObjectModel;
using Simple_Stream_UWP.Models;

namespace Simple_Stream_UWP.ViewModels
{
    public class GameDetailPageViewModel : ViewModel
    {
        #region Services & Repositories

        private readonly ITwitchRepository _twitchRepository;
        private readonly IPageDialogService _dialogService;

        #endregion

        #region Properties
        private StreamInformation latestSelectedStreamInformation;

        private string _currentGameName;

        public string CurrentGameName
        {
            get { return _currentGameName; }
            set { _currentGameName = value; OnPropertyChanged(); }
        }


        private ObservableCollection<StreamInformation> _streamInformations;

        public ObservableCollection<StreamInformation> StreamInformations
        {
            get { return _streamInformations; }
            set { _streamInformations = value; OnPropertyChanged(); }
        }

        private StreamInformation _selectedStreamInformation;

        public StreamInformation SelectedStreamInformation
        {
            get { return _selectedStreamInformation; }
            set { _selectedStreamInformation = value;  OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        #endregion

        public GameDetailPageViewModel(IDeviceGestureService gestureService, INavigationService navigationService, ITwitchRepository twitchRepository, IPageDialogService dialogService, ISessionStateService sessionStateService) : base(gestureService, navigationService, sessionStateService)
        {
            _twitchRepository = twitchRepository;
            _dialogService = dialogService;
        }
        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            CurrentGameName = e.Parameter.ToString(); // Game name.

            if (e.NavigationMode == Windows.UI.Xaml.Navigation.NavigationMode.Back || (_sessionStateService.SessionState.ContainsKey("latestLoadedGame") && _sessionStateService.SessionState["latestLoadedGame"].Equals(CurrentGameName)))
                return;

            // Loading mechanism could be better.
            IsBusy = true; 
            StreamInformations = await _twitchRepository.GetGameDetails(_currentGameName);
            _twitchRepository.SetLatestLoadedGames(StreamInformations);
            IsBusy = false;

            // Store latest loaded game for caching.
            if (_sessionStateService.SessionState.ContainsKey("latestLoadedGame"))
                _sessionStateService.SessionState["latestLoadedGame"] = CurrentGameName;
            else
                _sessionStateService.SessionState.Add("latestLoadedGame", CurrentGameName);

            this.PropertyChanged += PropChanged;
            
        }

        private void PropChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SelectedStreamInformation) && SelectedStreamInformation != null && latestSelectedStreamInformation != SelectedStreamInformation)
            { // New information selected, show options dialog and hide the previous one.
                if (latestSelectedStreamInformation != null)
                    latestSelectedStreamInformation.IsSelected = false;

                SelectedStreamInformation.IsSelected = true;

                latestSelectedStreamInformation = SelectedStreamInformation;
            }
        }
    }
}
