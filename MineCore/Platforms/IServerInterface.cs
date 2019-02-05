using MineCore.Models;
using MineCore.Platforms.Enums;

namespace MineCore.Platforms
{
    public interface IServerInterface : IMineCoreServiceProviderManager
    {
        StartResult StartServer(params string[] args);
        StopResult StopServer();
        StopResult StopServer(string reason);
    }
}