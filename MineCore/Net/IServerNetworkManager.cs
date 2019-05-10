using System;
using System.Net;

namespace MineCore.Net
{
    public interface IServerNetworkManager : IDisposable
    {
        IMineCraftProtocol Protocol { get; }
        IPEndPoint ServerEndPoint { get; }

        void Start();
        void UpdateServerList(IServerListData listData);
    }
}