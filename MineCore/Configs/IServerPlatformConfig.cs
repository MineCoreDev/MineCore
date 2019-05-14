namespace MineCore.Configs
{
    public interface IServerPlatformConfig : IConfig
    {
        string ServerName { get; set; }
        ushort ServerPort { get; set; }
        int ServerMaxPlayer { get; set; }
        bool UseXboxLiveAuth { get; set; }
        bool UseEncryption { get; set; }
    }
}