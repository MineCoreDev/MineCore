using MineCore.Languages;

namespace MineCore.Configs.Impl
{
    public class ServerPlatformConfig : Config, IServerPlatformConfig
    {
        public override string FileName => "server_config.json";
        public string ServerName { get; set; } = StringManager.GetString("minecore.list.serverName.default");
        public ushort ServerPort { get; set; } = 19132;
        public int ServerMaxPlayer { get; set; } = 20;
        public bool UseXboxLiveAuth { get; set; } = true;
        public bool UseEncryption { get; set; } = false;
    }
}