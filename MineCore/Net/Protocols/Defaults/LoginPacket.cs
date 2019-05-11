using BinaryIO;
using MineCore.Data.Impl;
using MineCore.Net.Impl;
using MineCore.Utils;

namespace MineCore.Net.Protocols.Defaults
{
    public class LoginPacket : DataPacket
    {
        public override byte PacketId => MineCraftProtocol.LOGIN_PACKET;

        public int Protocol { get; set; }

        public LoginData LoginData { get; set; }
        public ClientData ClientData { get; set; }

        public override void EncodePayload()
        {
            WriteInt(Protocol);
        }

        public override void DecodePayload()
        {
            LoginData.ThrownOnArgNull(nameof(LoginData));
            ClientData.ThrownOnArgNull(nameof(ClientData));

            Protocol = ReadInt();

            int len = ReadVarInt();
            using (BinaryStream stream = new BinaryStream(ReadBytes(len)))
            {
                int loginDataLen = stream.ReadInt(ByteOrder.Little);
                ReadLoginData(ReadBytes(loginDataLen));
                int clientDataLen = stream.ReadInt(ByteOrder.Little);
                ReadClientData(ReadBytes(clientDataLen));
            }
        }

        public void ReadLoginData(byte[] payload)
        {
        }

        public void WriteLoginData()
        {
        }

        public void ReadClientData(byte[] payload)
        {
        }

        public void WriteClientData()
        {
        }
    }
}