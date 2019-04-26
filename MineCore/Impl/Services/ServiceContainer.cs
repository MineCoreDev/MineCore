using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MineCore.Events;
using MineCore.Events.Services;
using MineCore.Services;
using MineCore.Utils;
using Optional;

namespace MineCore.Impl.Services
{
    public class ServiceContainer : IServiceContainer
    {
        private Dictionary<Guid, IMineCoreService> _services;

        public event EventHandler<ServiceLoadEventArgs> LoadServiceEvent;
        public event EventHandler<ServiceUnloadEventArgs> UnLoadServiceEvent;

        public ServiceContainerState ContainerState = ServiceContainerState.Initialized;

        public void LoadServices()
        {
            ContainerState = ServiceContainerState.ServiceLoading;

            Assembly asm = this.GetType().Assembly;
            IEnumerable<Type> services = asm.GetTypes()
                .Where((type) => type.GetInterfaces().Any(t => t == typeof(IMineCoreService)));

            foreach (Type service in services)
            {
                LoadService(service);
            }

            ContainerState = ServiceContainerState.ServiceLoaded;
        }

        public void UnloadServices()
        {
            ContainerState = ServiceContainerState.ServiceUnloading;

            foreach (Guid key in _services.Keys)
            {
                UnloadService(key);
            }

            ContainerState = ServiceContainerState.ServiceUnloaded;
        }

        public Option<IMineCoreService> GetService(Type type)
        {
            type.ThrownOnArgNull(nameof(type));

            if (_services.ContainsKey(type.GUID))
            {
                return _services[type.GUID].SomeNotNull();
            }

            return Option.None<IMineCoreService>();
        }

        public Option<IMineCoreService> GetService(Guid guid)
        {
            if (_services.ContainsKey(guid))
            {
                return _services[guid].SomeNotNull();
            }

            return Option.None<IMineCoreService>();
        }

        public Option<T> GetService<T>(Type type) where T : IMineCoreService
        {
            type.ThrownOnArgNull(nameof(type));

            if (_services.ContainsKey(type.GUID))
            {
                return ((T) _services[type.GUID]).SomeNotNull();
            }

            return Option.None<T>();
        }

        public Option<T> GetService<T>(Guid guid) where T : IMineCoreService
        {
            if (_services.ContainsKey(guid))
            {
                return ((T) _services[guid]).SomeNotNull();
            }

            return Option.None<T>();
        }

        public bool LoadService(Type type)
        {
            type.ThrownOnArgNull(nameof(type));

            if (type.GetInterfaces().Any(t => t == typeof(IMineCoreService)))
            {
                List<IMineCoreService> list = new List<IMineCoreService>();
                Queue<Type> queue = new Queue<Type>();
                queue.Enqueue(type);
                while (queue.Count > 0)
                {
                    Type t = queue.Dequeue();
                    if (_services.ContainsKey(t.GUID))
                    {
                        IMineCoreService service = (IMineCoreService) Activator.CreateInstance(t);
                        foreach (Type d in service.Dependencies)
                        {
                            queue.Enqueue(d);
                        }

                        service.OnLoad();
                        list.Add(service);
                    }
                }

                foreach (IMineCoreService service in list)
                {
                    service.OnEnable();
                    LoadServiceEvent.CancelableInvoke(this, new ServiceLoadEventArgs(this, service));
                    _services.Add(service.GetType().GUID, service);
                }

                return list.Count > 0;
            }

            return false;
        }

        public bool UnloadService(Type type)
        {
            type.ThrownOnArgNull(nameof(type));

            if (_services.ContainsKey(type.GUID))
            {
                return _services.Remove(type.GUID);
            }

            return false;
        }

        public bool UnloadService(Guid guid)
        {
            if (_services.ContainsKey(guid))
            {
                return _services.Remove(guid);
            }

            return false;
        }
    }
}