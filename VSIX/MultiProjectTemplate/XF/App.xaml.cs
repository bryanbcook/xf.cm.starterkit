using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using System.Reflection;
using Xamarin.Forms;
using $safeprojectname$.Utils;
using $safeprojectname$.ViewModels;

namespace $safeprojectname$
{
    public partial class App : FormsApplication
    {
        private readonly SimpleContainer container;

        public App(SimpleContainer container)
        {
            InitializeComponent();

            this.container = container;

            // TODO: Register additional viewmodels and services
            container
                // automatically register all viewmodels
                .AllTypesOf<BaseScreen>(GetType().GetTypeInfo().Assembly, ContainerRegistrationKind.PerRequest)
                // alternatively, register each viewmodel individually
                //.PerRequest<MainViewModel>()
                ;

            Initialize();

            DisplayRootViewFor<MainViewModel>();
        }

        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
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
