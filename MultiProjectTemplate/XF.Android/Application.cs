using Android.App;
using Android.Runtime;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace $safeprojectname$
{

    //You can specify additional application information in this attribute
#if DEBUG
    [Application(Debuggable = true)]
#else
    [Application(Debuggable = false)]
#endif
    public class MainApplication : CaliburnApplication
    {
        private SimpleContainer _container;

        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();

            // make the container available for resolution
            _container.Instance(_container);

            // CRITICAL! make sure our Xamarin.Forms App can only be initialized once!
            _container.Singleton<App>();

            // TODO: Register any Platform-Specific abstractions here
        }
        
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            // Get a list of all assemblies that will be used by the IoC container
            return new[]
            {
                GetType().Assembly,
                typeof(App).Assembly
            };
        }

        #region Caliburn IoC Methods
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
    }
}