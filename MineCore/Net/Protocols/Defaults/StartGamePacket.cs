using MineCore.Net.Impl;

namespace MineCore.Net.Protocols.Defaults
{
    public class StartGamePacket : DataPacket
    {
        public override byte PacketId => MineCraftProtocol.START_GAME_PACKET;

        public override void EncodePayload()
        {
        }

        public override void DecodePayload()
        {
        }
    }
}