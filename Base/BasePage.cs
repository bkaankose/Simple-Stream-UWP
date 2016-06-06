using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace Simple_Stream_UWP.Base
{
    /// <summary>
    /// BasePage implementation for all pages.
    /// This enabled a core animation for page transitions for now.
    /// </summary>
    public class BasePage : SessionStateAwarePage
    {
        internal Transition DEFAULT_TRANSITION = new PopupThemeTransition();
        public BasePage()
        {
            this.Transitions = new TransitionCollection() { DEFAULT_TRANSITION };
        }
    }
}
