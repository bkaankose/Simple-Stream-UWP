using Prism.Unity.Windows;
using Prism.Windows;
using Prism.Windows.Navigation;
using Simple_Stream_UWP.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Simple_Stream_UWP.Interfaces;
using Simple_Stream_UWP.Services;
using Microsoft.Practices.Unity;

namespace Simple_Stream_UWP
{
    sealed partial class App : PrismUnityApplication
    {
        public App()
        {
            InitializeComponent();
            ExtendedSplashScreenFactory = (splashscreen) => new ExtendedSplashScreen(splashscreen);
        }
        protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState != ApplicationExecutionState.Running)
                await this.InitializeServices();

            NavigationService.Navigate("Main", null);
        }

        private async Task InitializeServices()
        {
            // Here we're going to load the application and check the required connections.
            RegisterServices();

            // Load featured games for MainPage instead of doing it inside.
            await Container.Resolve<ITwitchRepository>().GetFeaturedGames();
        }

        private void RegisterServices()
        {
            Container.RegisterInstance(NavigationService);
            Container.RegisterInstance(SessionStateService);
            Container.RegisterInstance(DeviceGestureService);

            Container.RegisterInstance<ITwitchService>(new TwitchService());
            Container.RegisterInstance<ITwitchRepository>(new TwitchRepository(Container.Resolve<ITwitchService>()));
            Container.RegisterInstance<IPageDialogService>(new PageDialogService());
       }
    }
}
