using System;
using MineCore.Events.Services;
using NLog;
using Optional;

namespace MineCore.Services
{
    public interface IServiceContainer
    {
        Logger ContainerLogger { get; }

        event EventHandler<ServiceLoadEventArgs> LoadServiceEvent;
        event EventHandler<ServiceUnloadEventArgs> UnLoadServiceEvent;

        void LoadServices();
        void UnloadServices();

        Option<IMineCoreService> GetService(Type type);
        Option<IMineCoreService> GetService(Guid guid);
        Option<T> GetService<T>(Type type) where T : IMineCoreService;
        Option<T> GetService<T>(Guid guid) where T : IMineCoreService;

        bool LoadService(Type type);

        bool UnloadService(Type type);
        bool UnloadService(Guid guid);
    }
}