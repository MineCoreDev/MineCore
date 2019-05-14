using System;
using System.Net;
using MineCore.Configs;

namespace MineCore.Net
{
    public interface IServerNetworkManager : IDisposable
    {
        IServerPlatformConfig ServerConfig { get; }
        IMineCraftProtocol Protocol { get; }
        IServerListData ServerListData { get; }
        IPEndPoint ServerEndPoint { get; }

        void Start();
        void UpdateServerList();
    }
}