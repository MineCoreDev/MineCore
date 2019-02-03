using System;
using MineCore.Events;

namespace MineCore
{
    public interface IServiceProvider
    {
        event EventHandler<ServiceProviderEventArgs> ServiceEnabled;
        event EventHandler<ServiceProviderEventArgs> ServiceDisabled;
    }
}