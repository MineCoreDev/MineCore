using System;

namespace MineCore.Events
{
    public class ServiceProviderEventArgs : EventArgs
    {
        public IServiceProvider ServiceProvider { get; }

        public ServiceProviderEventArgs(IServiceProvider provider)
        {
            ServiceProvider = provider;
        }
    }
}