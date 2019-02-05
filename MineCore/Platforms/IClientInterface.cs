using MineCore.Models;
using MineCore.Platforms.Enums;

namespace MineCore.Platforms
{
    public interface IClientInterface : IMineCoreServiceProviderManager
    {
        StartResult StartClient(params string[] args);
        StopResult StopClient();
        StopResult StopClient(string reason);
    }
}