using MineCore.Net;

namespace MineCore.Platforms
{
    public interface IServerPlatform : IPlatform
    {
        IMineCraftProtocol Protocol { get; }
        IServerNetworkManager NetworkManager { get; }
    }
}