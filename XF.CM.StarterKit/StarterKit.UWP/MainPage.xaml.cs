using Caliburn.Micro;
using Shared = XF.CM.StarterKit; // required for VSTemplate

namespace XF.CM.StarterKit.UWP
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
