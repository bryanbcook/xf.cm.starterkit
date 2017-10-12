using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace $safeprojectname$
{
    class CaliburnAppDelegate : CaliburnApplicationDelegate
    {
        private SimpleContainer _container;

        public CaliburnAppDelegate()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();
            _container.Instance(_container);

            _container.Singleton<App>();

            // TODO: Register platform specific dependencies here
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                GetType().Assembly,
                typeof(App).Assembly
            };
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
    }
}