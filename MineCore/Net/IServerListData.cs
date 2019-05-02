namespace MineCore.Net
{
    public interface IServerListData
    {
        string Platform { get; }
        string ServerName { get; }
        int ProtocolNumber { get; }
        string Version { get; }
        int JoinedPlayer { get; }
        int MaxPlayer { get; }
        string SystemName { get; }
        string ServerSubName { get; }

        string GetString();
    }
}