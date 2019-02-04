using System;
using MineCore.Events;

namespace MineCore
{
    public abstract class MineCoreServiceProvider : IMineCoreServiceProvider
    {
        public virtual string ServiceName { get; }
        public virtual Type[] Dependencies { get; }
        public event EventHandler<ServiceProviderEventArgs> ServiceEnabled;
        public event EventHandler<ServiceProviderEventArgs> ServiceDisabled;
    }
}