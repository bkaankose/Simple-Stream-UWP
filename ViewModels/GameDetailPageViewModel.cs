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


namespace Simple_Stream_UWP.ViewModels
{
    public class GameDetailPageViewModel : ViewModel
    {
        #region Services & Repositories

        private readonly ITwitchRepository _twitchRepository;
        private readonly IPageDialogService _dialogService;

        #endregion

        #region Properties
        private string _currentGameName = string.Empty;
        

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
            _currentGameName = e.Parameter.ToString(); // Game name.
            var det = await _twitchRepository.GetGameDetails(_currentGameName);
        }
    }
}
