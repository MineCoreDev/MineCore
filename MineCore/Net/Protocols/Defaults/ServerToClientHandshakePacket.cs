using MineCore.Net.Impl;

namespace MineCore.Net.Protocols.Defaults
{
    public class ServerToClientHandshakePacket : DataPacket
    {
        public override byte PacketId => MineCraftProtocol.SERVER_TO_CLIENT_HANDSHAKE_PACKET;

        public string Token { get; set; }

        public override void EncodePayload()
        {
            WriteString(Token);
        }

        public override void DecodePayload()
        {
            Token = ReadString();
        }
    }
}