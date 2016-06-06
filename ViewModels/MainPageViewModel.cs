using Prism.Commands;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using Simple_Stream_UWP.Base;
using Simple_Stream_UWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Simple_Stream_UWP.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        #region Services & Repositories

        private readonly ITwitchRepository _twitchRepository;
        private readonly IPageDialogService _dialogService;

        public MainPageViewModel(IDeviceGestureService gestureService,INavigationService navigationService, ITwitchRepository twitchRepository,IPageDialogService dialogService) : base(gestureService,navigationService)
        {
            _twitchRepository = twitchRepository;
            _dialogService = dialogService;
            BackCommand = new DelegateCommand(() => TerminateAppliation());
        }

        #endregion

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            var games = await _twitchRepository.GetFeaturedGames();
        }

        private async void TerminateAppliation()
        {
            var exitComfirmed = await _dialogService.DisplayConfirmationDialogAsync("Do you want to exit from the application?","Are you sure?");
            if (exitComfirmed)
                Application.Current.Exit();
        }
    }
}
