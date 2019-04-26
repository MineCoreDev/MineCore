using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MineCore.Events.Services;
using MineCore.Services;
using Optional;

namespace MineCore.Impl.Services
{
    public class ServiceContainer : IServiceContainer
    {
        public event EventHandler<ServiceLoadEventArgs> LoadServiceEvent;
        public event EventHandler<ServiceUnloadEventArgs> UnLoadServiceEvent;

        public ServiceContainerState ContainerState = ServiceContainerState.Initialized;

        public void LoadServices()
        {
            Assembly asm = this.GetType().Assembly;
            IEnumerable<Type> services = asm.GetTypes()
                .Where((type) => type.GetInterfaces().Any(t => t == typeof(IMineCoreService)));
        }

        public void UnloadServices()
        {
            throw new NotImplementedException();
        }

        public Option<IMineCoreService> GetService(Type type)
        {
            throw new NotImplementedException();
        }

        public Option<IMineCoreService> GetService(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Option<T> GetService<T>(Type type) where T : IMineCoreService
        {
            throw new NotImplementedException();
        }

        public Option<T> GetService<T>(Guid guid) where T : IMineCoreService
        {
            throw new NotImplementedException();
        }

        public bool LoadService(Type type)
        {
            throw new NotImplementedException();
        }

        public bool LoadService(Guid type)
        {
            throw new NotImplementedException();
        }

        public bool UnloadService(Type type)
        {
            throw new NotImplementedException();
        }

        public bool UnloadService(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}