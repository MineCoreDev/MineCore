using MineCore.Network.Protocols;

namespace MineCore.Factories
{
    public interface IPacketFactory : IFactory<int, DataPacket>
    {
    }
}