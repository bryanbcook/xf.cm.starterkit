using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.ViewModels
{
    public class MainViewModel : BaseScreen
    {
        private string _label;

        public MainViewModel()
        {
            DisplayName = "Welcome!";
            IntroLabel = "Hello World via Caliburn.Micro!";
        }        

        public string IntroLabel
        {
            get { return _label; }
            set { Set(ref _label, value); }
        }

    }
}
