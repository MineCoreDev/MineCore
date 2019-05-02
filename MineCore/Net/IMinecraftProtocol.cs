namespace MineCore.Net
{
    public interface IMineCraftProtocol
    {
        int ProtocolNumber { get; }

        string Version { get; }
    }
}