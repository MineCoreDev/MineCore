using MineCore.Net.Impl;

namespace MineCore.Net.Protocols.Defaults
{
    public class ClientToServerHandshakePacket : DataPacket
    {
        public override byte PacketId => MineCraftProtocol.CLIENT_TO_SERVER_HANDSHAKE_PACKET;

        public override void EncodePayload()
        {
        }

        public override void DecodePayload()
        {
        }
    }
}