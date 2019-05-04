using MineCore.Net;

namespace MineCore.Platforms
{
    public interface IServerPlatform : IPlatform
    {
        IServerNetworkManager NetworkManager { get; }
    }
}