using Caliburn.Micro;
using Shared = $ext_safeprojectname$; // required for VSTemplate

namespace $safeprojectname$
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(IoC.Get<Shared.App>());
        }
    }
}
