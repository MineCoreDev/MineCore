using MineCore.Configs;
using MineCore.Extensions;
using MineCore.Factories;
using MineCore.Network;
using MineCore.Worlds;

namespace MineCore.Platforms
{
    public interface IPlatform
    {
        IConfigManager ConfigManager { get; set; }
        INetworkManager NetworkManager { get; set; }
        IExtensionManager ExtensionManager { get; set; }
        IFactoryManager FactoryManager { get; set; }
        IWorldManager WorldManager { get; set; }
    }
}