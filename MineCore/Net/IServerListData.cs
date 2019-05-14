namespace MineCore.Net
{
    public interface IServerListData
    {
        string Platform { get; }
        string ServerName { get; set; }
        int ProtocolNumber { get; }
        string Version { get; }
        int JoinedPlayer { get; set; }
        int MaxPlayer { get; set; }
        string SystemName { get; }
        string ServerSubName { get; set; }

        string GetString();
    }
}