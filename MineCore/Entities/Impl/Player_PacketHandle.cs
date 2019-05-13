using MineCore.Net.Protocols;
using MineCore.Net.Protocols.Defaults;
using MineCore.Utils;

namespace MineCore.Entities.Impl
{
    public partial class Player
    {
        public void HandleDataPacket(DataPacket packet)
        {
            packet.ThrownOnArgNull(nameof(packet));

            if (packet is LoginPacket loginPacket)
            {
                OnLoginPacket(loginPacket);
            }
        }

        private void OnLoginPacket(LoginPacket packet)
        {
            PlayStatusPacket pk = new PlayStatusPacket();
            if (packet.Protocol > Protocol.ProtocolNumber)
            {
                pk.Status = PlayStatusPacket.LOGIN_FAILED_SERVER;
                SendDataPacket(pk);
                return;
            }

            if (packet.Protocol < Protocol.ProtocolNumber)
            {
                pk.Status = PlayStatusPacket.LOGIN_FAILED_CLIENT;
                SendDataPacket(pk);
                return;
            }

            // サーバーが満員の時に
            // pk.Status = PlayStatusPacket.LOGIN_FAILED_SERVER_FULL;
            // SendDataPacket(pk);
            // return;

            LoginData = packet.LoginData;
            ClientData = packet.ClientData;

            // 暗号化

            pk.Status = PlayStatusPacket.LOGIN_SUCCESS;
            SendDataPacket(pk);

            ResourcePacksInfoPacket pk2 = new ResourcePacksInfoPacket();
            SendDataPacket(pk2);
        }
    }
}