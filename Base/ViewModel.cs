using Prism.Commands;
using Prism.Mvvm;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Simple_Stream_UWP.Base
{
    /// <summary>
    /// Base view model class for all view model implementations.
    /// Enables common initializations.
    /// </summary>
    public class ViewModel : BindableBase, INavigationAware
    {
        internal IDeviceGestureService _gestureService;
        internal INavigationService _navigationService;
        internal ISessionStateService _sessionStateService;

        //public DelegateCommand BackCommand { get; set; } // Default back command for specific page.

        // General busy indicator property for specific view model.
        private bool _isBusy;
        private IDeviceGestureService gestureService;
        private INavigationService navigationService;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public ViewModel(IDeviceGestureService gestureService,INavigationService navigationService,ISessionStateService sessionStateService)
        {
            _gestureService = gestureService;
            _navigationService = navigationService;
            _sessionStateService = sessionStateService;
        }

        public virtual void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            
        }
        public virtual void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            
        }
    }
}
