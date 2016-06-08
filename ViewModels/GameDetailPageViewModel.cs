using Simple_Stream_UWP.Base;
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

        #endregion

        #region Commands

        #endregion

        public GameDetailPageViewModel(IDeviceGestureService gestureService, INavigationService navigationService, ITwitchRepository twitchRepository, IPageDialogService dialogService) : base(gestureService, navigationService)
        {
            _twitchRepository = twitchRepository;
            _dialogService = dialogService;
            BackCommand = new DelegateCommand(() => _navigationService.GoBack());
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            CurrentGameName = e.Parameter.ToString(); // Game name.

            IsBusy = true;
            StreamInformations = await _twitchRepository.GetGameDetails(_currentGameName);
            IsBusy = false;
        }
    }
}
