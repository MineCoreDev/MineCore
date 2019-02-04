using System;
using MineCore.Events;

namespace MineCore
{
    public interface IMineCoreServiceProvider
    {
        string ServiceName { get; }
        event EventHandler<ServiceProviderEventArgs> ServiceEnabled;
        event EventHandler<ServiceProviderEventArgs> ServiceDisabled;
    }
}