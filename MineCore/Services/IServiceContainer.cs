using System;
using MineCore.Datas;
using MineCore.Events.Services;

namespace MineCore.Services
{
    public interface IServiceContainer : IName
    {
        event EventHandler<ServiceLoadEventArgs> LoadServiceEvent;
        event EventHandler<ServiceUnloadEventArgs> UnLoadServiceEvent;

        void LoadServices();
        void UnloadServices();

        IMineCoreService GetService(Type type);
        IMineCoreService GetService(Guid guid);

        bool LoadService(Type type);
        bool LoadService(Guid type);

        bool UnloadService(Type type);
        bool UnloadService(Guid guid);
    }
}