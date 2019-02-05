using System;
using MineCore.Models;

namespace MineCore.Events
{
    public class ServiceProviderEventArgs : EventArgs
    {
        public IMineCoreServiceProvider ServiceProvider { get; }

        public ServiceProviderEventArgs(IMineCoreServiceProvider provider)
        {
            ServiceProvider = provider;
        }
    }
}