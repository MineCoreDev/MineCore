using System;
using MineCore.Events.Services;
using MineCore.Services;

namespace MineCore.Impl.Services
{
    public class ServiceContainer : IServiceContainer
    {
        public event EventHandler<ServiceLoadEventArgs> LoadServiceEvent;
        public event EventHandler<ServiceUnloadEventArgs> UnLoadServiceEvent;

        public void LoadServices()
        {
            throw new NotImplementedException();
        }

        public void UnloadServices()
        {
            throw new NotImplementedException();
        }

        public IMineCoreService GetService(Type type)
        {
            throw new NotImplementedException();
        }

        public IMineCoreService GetService(Guid guid)
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