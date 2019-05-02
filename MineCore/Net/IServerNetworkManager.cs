using System;

namespace MineCore.Net
{
    public interface IServerNetworkManager : IDisposable
    {
        void Start();
        void UpdateServerList(IServerListData listData);
    }
}