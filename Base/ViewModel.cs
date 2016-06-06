using Prism.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Base
{
    /// <summary>
    /// Base view model class for all view model implementations.
    /// Enables common initializations.
    /// </summary>
    public class ViewModel : BindableBase, INavigationAware
    {
        public ViewModel()
        {

        }
        public void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            
        }

        public void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            
        }
    }
}
