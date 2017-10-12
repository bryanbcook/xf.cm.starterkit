using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using System.Reflection;
using Xamarin.Forms;
using XF.CM.StarterKit.Utils;
using XF.CM.StarterKit.ViewModels;

namespace XF.CM.StarterKit
{
    public partial class App : FormsApplication
    {
        private readonly SimpleContainer _container;

        public App(SimpleContainer container)
        {
            InitializeComponent();

            this._container = container;

            // TODO: Register additional viewmodels and services
            container
                // automatically register all viewmodels
                .AllTypesOf<BaseScreen>(GetType().GetTypeInfo().Assembly, ContainerRegistrationKind.PerRequest)
                // alternatively, register each viewmodel individually
                //.PerRequest<MainViewModel>()
                ;

            // setup root page as a navigation page
            PrepareViewFirst();
            
            // navigate to main view
            container.GetInstance<INavigationService>()
                .For<MainViewModel>()
                .Navigate(false);
        }

        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            // register the navigation service
            _container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
