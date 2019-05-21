namespace MineCore.Network
{
    public interface INetworkManager
    {
        IProtocol Protocol { get; set; }
    }
}