﻿using Prism.Commands;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using Simple_Stream_UWP.Base;
using Simple_Stream_UWP.Interfaces;
using Simple_Stream_UWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Simple_Stream_UWP.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        #region Properties

        private string _queryString;

        public string QueryString
        {
            get { return _queryString; }
            set { _queryString = value; OnPropertyChanged(); }
        }

        private ObservableCollection<FeaturedGame> _featuredGames;

        public ObservableCollection<FeaturedGame> FeaturedGames
        {
            get { return _featuredGames; }
            set { _featuredGames = value; OnPropertyChanged(); }
        }

        #endregion
        #region Services & Repositories

        private readonly ITwitchRepository _twitchRepository;
        private readonly IPageDialogService _dialogService;

        #endregion
        #region Commands

        public DelegateCommand QueryCommand { get; set; }
        public DelegateCommand RefreshFeaturedGamesCommand { get; set; }

        #endregion
        public MainPageViewModel(IDeviceGestureService gestureService,INavigationService navigationService, ITwitchRepository twitchRepository,IPageDialogService dialogService) : base(gestureService,navigationService)
        {
            _twitchRepository = twitchRepository;
            _dialogService = dialogService;
            BackCommand = new DelegateCommand(() => TerminateAppliation());
            QueryCommand = new DelegateCommand(Query);
            RefreshFeaturedGamesCommand = new DelegateCommand(async () => await RefreshFeaturedGames());
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            FeaturedGames = await _twitchRepository.GetFeaturedGames();
        }
        
        private async Task RefreshFeaturedGames()
        {
            await _twitchRepository.ReloadFeaturedGames();
            FeaturedGames = await _twitchRepository.GetFeaturedGames();
        }

        private async void TerminateAppliation()
        {
            var exitComfirmed = await _dialogService.DisplayConfirmationDialogAsync("Do you want to exit from the application?","Are you sure?");
            if (exitComfirmed)
                Application.Current.Exit();
        }

        private void Query()
        {

        }
    }
}
