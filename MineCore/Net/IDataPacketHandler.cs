using MineCore.Entities;
using MineCore.Net.Protocols;

namespace MineCore.Net
{
    public interface IDataPacketHandler
    {
        void HandleDataPacket(DataPacket packet);
    }
}