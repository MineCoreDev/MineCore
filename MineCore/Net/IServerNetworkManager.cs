using System;
using System.Net;

namespace MineCore.Net
{
    public interface IServerNetworkManager : IDisposable
    {
        IPEndPoint ServerEndPoint { get; }

        void Start();
        void UpdateServerList(IServerListData listData);
    }
}