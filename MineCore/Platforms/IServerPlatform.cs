using MineCore.Configs;
using MineCore.Net;

namespace MineCore.Platforms
{
    public interface IServerPlatform : IPlatform
    {
        IServerPlatformConfig ServerConfig { get; }
        IMineCraftProtocol Protocol { get; }
        IServerNetworkManager NetworkManager { get; }
    }
}