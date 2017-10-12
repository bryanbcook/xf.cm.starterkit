using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Utils
{
    public static class SimpleContainerExtensions
    {
        public static SimpleContainer AllTypesOf<TService>(this SimpleContainer target, Assembly assembly, ContainerRegistrationKind kind = ContainerRegistrationKind.PerRequest)
        {
            var serviceType = typeof(TService).GetTypeInfo();

            var types = assembly.DefinedTypes
                .Where(type => serviceType.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface);

            foreach (var typeInfo in types)
            {
                var type = typeInfo.AsType();
                if (kind == ContainerRegistrationKind.PerRequest)
                {
                    target.RegisterPerRequest(type, null, type);
                }
                else
                {
                    target.RegisterSingleton(type, null, type);
                }
            }

            return target;
        }
    }

    public enum ContainerRegistrationKind
    {
        Singleton,
        PerRequest
    }
}
