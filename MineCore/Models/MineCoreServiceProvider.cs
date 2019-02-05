using System;
using MineCore.Events;

namespace MineCore.Models
{
    public abstract class MineCoreServiceProvider : IMineCoreServiceProvider
    {
        public virtual string ServiceName { get; }
        public virtual Type[] Dependencies { get; }
        public event EventHandler<ServiceProviderEventArgs> ServiceEnabled;
        public event EventHandler<ServiceProviderEventArgs> ServiceDisabled;

        public virtual void OnServiceEnabled(object sender, ServiceProviderEventArgs args)
        {
            ServiceEnabled?.Invoke(sender, args);
        }

        public virtual void OnServiceDisabled(object sender, ServiceProviderEventArgs args)
        {
            ServiceDisabled?.Invoke(sender, args);
        }
    }
}