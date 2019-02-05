using System;
using MineCore.Models;

namespace MineCore.Events
{
    public class ServiceProviderManagerEventArgs : EventArgs
    {
        public IMineCoreServiceProvider[] ServiceProviders { get; }

        public ServiceProviderManagerEventArgs(IMineCoreServiceProvider[] providers)
        {
            ServiceProviders = providers;
        }
    }
}