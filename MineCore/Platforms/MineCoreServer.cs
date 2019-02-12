using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MineCore.Events;
using MineCore.Extensions;
using MineCore.Models;
using MineCore.Platforms.Enums;

namespace MineCore.Platforms
{
    public class MineCoreServer : IServerInterface
    {
        private readonly List<IMineCoreServiceProvider> _internalServices = new List<IMineCoreServiceProvider>();
        private readonly List<IMineCoreServiceProvider> _externalServices = new List<IMineCoreServiceProvider>();

        // TODO Cache Data
        public IMineCoreServiceProvider[] ServiceProviders => _internalServices.Concat(_externalServices).ToArray();
        public string ExternalServiceFolderPath => "plugins";

        public event EventHandler<ServiceProviderManagerEventArgs> LoadInternalServices;
        public event EventHandler<ServiceProviderManagerEventArgs> UnLoadInternalServices;
        public event EventHandler<ServiceProviderManagerEventArgs> LoadExternalServices;
        public event EventHandler<ServiceProviderManagerEventArgs> UnLoadExternalServices;

        public virtual void LoadingInternalServices()
        {
            Assembly assembly = GetType().Assembly;
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

            _internalServices.AddRange(providers);

            OnLoadInternalServices(this, new ServiceProviderManagerEventArgs(providers));
        }

        // TODO Dependencies
        public virtual void LoadingExternalServices()
        {
            string path = Path.Combine(Environment.CurrentDirectory, ExternalServiceFolderPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles()
                .Where(file => file.Extension == ExtensionConstants.PackFileExtension).ToArray();
            Assembly[] assemblies = files.Select(f => Assembly.LoadFile(f.FullName)).ToArray();
            Type[][] types = assemblies
                .Select(asm => asm.GetTypes()
                    .Where(type => type.GetInterfaces()
                        .Any(t => t == typeof(IMineCoreServiceProvider) &&
                                  type.GetCustomAttributes(typeof(ServerSideServiceAttribute), false)
                                      .Length > 0)).ToArray()).ToArray();
            List<IMineCoreServiceProvider> externalServices = new List<IMineCoreServiceProvider>();
            foreach (Type[] arr in types)
            {
                IMineCoreServiceProvider[] providers = arr.Select(type =>
                {
                    IMineCoreServiceProvider provider = (IMineCoreServiceProvider) Activator.CreateInstance(type);
                    provider.OnServiceEnabled(this, new ServiceProviderEventArgs(provider));

                    return provider;
                }).ToArray();

                externalServices.AddRange(providers);
            }

            IMineCoreServiceProvider[] builded = externalServices.ToArray();
            _externalServices.AddRange(builded);

            OnLoadExternalServices(this, new ServiceProviderManagerEventArgs(builded));
        }

        public virtual void UnloadingInternalServices()
        {
            foreach (IMineCoreServiceProvider service in _internalServices)
            {
                service.OnServiceDisabled(this, new ServiceProviderEventArgs(service));
            }

            OnUnLoadInternalServices(this, new ServiceProviderManagerEventArgs(_internalServices.ToArray()));

            _internalServices.Clear();
        }

        public virtual void UnloadingExternalServices()
        {
            foreach (IMineCoreServiceProvider service in _externalServices)
            {
                service.OnServiceDisabled(this, new ServiceProviderEventArgs(service));
            }

            OnUnLoadExternalServices(this, new ServiceProviderManagerEventArgs(_externalServices.ToArray()));

            _externalServices.Clear();
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