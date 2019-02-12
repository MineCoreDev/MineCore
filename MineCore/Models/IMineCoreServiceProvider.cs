using System;
using MineCore.Events;

namespace MineCore.Models
{
    public interface IMineCoreServiceProvider
    {
        string ServiceName { get; }
        Type[] Dependencies { get; }
        event EventHandler<ServiceProviderEventArgs> ServiceEnabled;
        event EventHandler<ServiceProviderEventArgs> ServiceDisabled;
        event EventHandler<ServiceProviderEventArgs> ServiceDependentResolution;

        void OnServiceEnabled(object sender, ServiceProviderEventArgs args);
        void OnServiceDisabled(object sender, ServiceProviderEventArgs args);
        void OnServiceDependentResolution(object sender, ServiceProviderEventArgs args);
    }
}