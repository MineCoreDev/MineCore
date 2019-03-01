using MineCore.Models;
using MineCore.Platforms;

namespace MineCore.Network
{
    [ServerSideService, ClientSideService]
    public class NetworkService : MineCoreServiceProvider, INetworkInterface
    {
    }
}