namespace MineCore.Network
{
    public interface IProtocol
    {
        IServerList ServerList { get; set; }
    }
}