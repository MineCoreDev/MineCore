using MineCore.Net.Impl;

namespace MineCore.Net.Protocols.Defaults
{
    public class DisconnectPacket : DataPacket
    {
        public override byte PacketId => MineCraftProtocol.DISCONNECT_PACKET;

        public bool HideScreen { get; set; }
        public string Message { get; set; }

        public override void EncodePayload()
        {
            WriteBoolean(HideScreen);
            if (!HideScreen)
            {
                WriteString(Message);
            }
        }

        public override void DecodePayload()
        {
            HideScreen = ReadBoolean();
            if (!HideScreen)
            {
                Message = ReadString();
            }
        }
    }
}