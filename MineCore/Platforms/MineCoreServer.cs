using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MineCore.Events;
using MineCore.Models;
using MineCore.Platforms.Enums;

namespace MineCore.Platforms
{
    public class MineCoreServer : IServerInterface
    {
        private readonly List<IMineCoreServiceProvider> _services = new List<IMineCoreServiceProvider>();

        public IMineCoreServiceProvider[] ServiceProviders => _services.ToArray();

        public event EventHandler<ServiceProviderManagerEventArgs> LoadInternalServices;
        public event EventHandler<ServiceProviderManagerEventArgs> UnLoadInternalServices;
        public event EventHandler<ServiceProviderManagerEventArgs> LoadExternalServices;
        public event EventHandler<ServiceProviderManagerEventArgs> UnLoadExternalServices;

        public virtual void LoadingInternalServices()
        {
            Assembly assembly = this.GetType().Assembly;
            Type[] types = assembly.GetTypes()
                .Where(type => type.GetInterfaces()
                    .Any(t => t == typeof(IMineCoreServiceProvider) &&
                              type.GetCustomAttributes(typeof(ServerSideServiceAttribute), false)
                                  .Length > 0)).ToArray();
            IMineCoreServiceProvider[] providers = types.Select(t =>
            {
                IMineCoreServiceProvider provider = (IMineCoreServiceProvider) Activator.CreateInstance(t);
                provider.OnServiceEnabled(this, new ServiceProviderEventArgs(provider));

                return provider;
            }).ToArray();

            _services.AddRange(providers);

            OnLoadInternalServices(this, new ServiceProviderManagerEventArgs(providers));
        }

        public virtual void LoadingExternalServices()
        {
            throw new NotImplementedException();
        }

        public virtual void UnloadingInternalServices()
        {
            throw new NotImplementedException();
        }

        public virtual void UnloadingExternalServices()
        {
            throw new NotImplementedException();
        }

        public virtual StartResult StartServer(params string[] args)
        {
            throw new NotImplementedException();
        }

        public virtual StopResult StopServer()
        {
            throw new NotImplementedException();
        }

        public virtual StopResult StopServer(string reason)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnLoadInternalServices(object sender, ServiceProviderManagerEventArgs args)
        {
            LoadInternalServices?.Invoke(sender, args);
        }

        protected virtual void OnUnLoadInternalServices(object sender, ServiceProviderManagerEventArgs args)
        {
            UnLoadInternalServices?.Invoke(sender, args);
        }

        protected virtual void OnLoadExternalServices(object sender, ServiceProviderManagerEventArgs args)
        {
            LoadExternalServices?.Invoke(sender, args);
        }

        protected virtual void OnUnLoadExternalServices(object sender, ServiceProviderManagerEventArgs args)
        {
            UnLoadExternalServices?.Invoke(sender, args);
        }
    }
}