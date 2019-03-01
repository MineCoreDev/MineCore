using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MineCore.Events;
using MineCore.Extensions;
using MineCore.Models;
using MineCore.Platforms.Enums;
using NLog;

namespace MineCore.Platforms
{
    public class MineCoreServer : IServerInterface
    {
        public readonly static ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly ConcurrentDictionary<string, IMineCoreServiceProvider> _internalServices =
            new ConcurrentDictionary<string, IMineCoreServiceProvider>();

        private readonly ConcurrentDictionary<string, IMineCoreServiceProvider> _externalServices =
            new ConcurrentDictionary<string, IMineCoreServiceProvider>();

        public IMineCoreServiceProvider[] ServiceProviders =>
            _internalServices.Values.Concat(_externalServices.Values).ToArray();

        public string ExternalServiceFolderPath => "extensions";

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

            foreach (Type type in types)
            {
                try
                {
                    IMineCoreServiceProvider provider =
                        (IMineCoreServiceProvider) Activator.CreateInstance(type);
                    LoadingService(provider, _internalServices);
                }
                catch (InvalidCastException e)
                {
                    Logger.Warn(e);
                }
            }

            OnLoadInternalServices(this,
                new ServiceProviderManagerEventArgs(_internalServices.Values.ToArray()));
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

            foreach (Type[] arr in types)
            {
                foreach (Type type in arr)
                {
                    try
                    {
                        IMineCoreServiceProvider provider =
                            (IMineCoreServiceProvider) Activator.CreateInstance(type);
                        LoadingService(provider, _externalServices);
                    }
                    catch (InvalidCastException e)
                    {
                        Logger.Warn(e);
                    }
                }
            }

            OnLoadExternalServices(this,
                new ServiceProviderManagerEventArgs(_externalServices.Values.ToArray()));
        }

        private void LoadingService(IMineCoreServiceProvider provider,
            ConcurrentDictionary<string, IMineCoreServiceProvider> services)
        {
            Queue<IMineCoreServiceProvider> queue = new Queue<IMineCoreServiceProvider>();
            queue.Enqueue(provider);

            while (queue.TryDequeue(out IMineCoreServiceProvider p))
            {
                if (!services.ContainsKey(p.ServiceName))
                {
                    services.TryAdd(p.ServiceName, p);
                    foreach (Type type in p.Dependencies)
                    {
                        try
                        {
                            IMineCoreServiceProvider dp =
                                (IMineCoreServiceProvider) Activator.CreateInstance(type);
                            if (!services.ContainsKey(dp.ServiceName))
                            {
                                dp.OnServiceEnabled(this, new ServiceProviderEventArgs(dp));
                                queue.Enqueue(dp);
                            }
                        }
                        catch (InvalidCastException e)
                        {
                            Logger.Warn(e);
                        }
                    }
                }
            }

            provider.OnServiceEnabled(this, new ServiceProviderEventArgs(provider));
        }

        public virtual void UnloadingInternalServices()
        {
            foreach (IMineCoreServiceProvider service in _internalServices.Values)
            {
                service.OnServiceDisabled(this, new ServiceProviderEventArgs(service));
            }

            OnUnLoadInternalServices(this,
                new ServiceProviderManagerEventArgs(_internalServices.Values.ToArray()));
            _internalServices.Clear();
        }

        public virtual void UnloadingExternalServices()
        {
            foreach (IMineCoreServiceProvider service in _externalServices.Values)
            {
                service.OnServiceDisabled(this, new ServiceProviderEventArgs(service));
            }

            OnUnLoadExternalServices(this,
                new ServiceProviderManagerEventArgs(_externalServices.Values.ToArray()));
            _externalServices.Clear();
        }

        public virtual StartResult StartServer(params string[] args)
        {
            
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