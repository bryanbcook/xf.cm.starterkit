using System;

namespace $safeprojectname$.Services
{
    /// <summary>
    /// An application service is an in-memory service that is initialized with the application
    /// </summary>
    /// <remarks>
    /// Classes that implement <see cref="IApplicationService"/> are treated as singletons that are available with the application.
    /// They may act as stand-alone services that listen for or publish events, or may be injected into viewmodels as constructor dependencies.
    /// </remarks>
    public interface IApplicationService : IDisposable
    {
        /// <summary>
        /// Initializes the service synchronously on application startup.
        /// </summary>
        void Initialize();
    }
}
