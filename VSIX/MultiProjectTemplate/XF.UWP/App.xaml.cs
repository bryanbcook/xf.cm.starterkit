using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Shared = $ext_safeprojectname$;

namespace $safeprojectname$
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App 
    {
        private WinRTContainer _container;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            // enable if desired...
            //if (Debugger.IsAttached)
            //{
            //    DebugSettings.EnableFrameRateCounter = true;
            //}
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // hide windows phone status bar
                if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
                {
                    var statusBar = StatusBar.GetForCurrentView();
                    await statusBar.HideAsync();
                }

                // change color of uwp title bar
                if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
                {
                    var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                    if (titleBar != null)
                    {
                        var desiredColor = Color.FromArgb(255, 00, 96, 180); // deep blue
                        titleBar.BackgroundColor = desiredColor;
                        titleBar.ButtonBackgroundColor = desiredColor;
                        titleBar.ForegroundColor = Colors.White;
                        titleBar.ButtonForegroundColor = Colors.White;
                    }
                }

                Initialize(); // initiliaze Caliburn.Micro
                Xamarin.Forms.Forms.Init(e, RendererAssemblies());
                // TODO: Initialize other xamarin.form 3rd party components

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        #region Caliburn IOC configuration and overloads
        protected override void Configure()
        {
            _container = new WinRTContainer();
            _container.RegisterInstance(typeof(WinRTContainer), null, _container);
            _container.RegisterInstance(typeof(SimpleContainer), null, _container);

        _container.Singleton<Shared.App>();

            // TODO: Register Platform-specific dependencies here
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            yield return GetType().GetTypeInfo().Assembly;
            yield return typeof(Shared.App).GetTypeInfo().Assembly;
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }
        #endregion

        private IEnumerable<Assembly> RendererAssemblies()
        {
            // TODO: Ensure any custom renderer assemblies (3rd party components, etc) are added to the renderer list
            return Enumerable.Empty<Assembly>();
        }
    }
}
