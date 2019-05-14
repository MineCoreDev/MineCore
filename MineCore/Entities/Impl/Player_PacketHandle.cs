using System;
using MineCore.Net.Protocols;
using MineCore.Net.Protocols.Defaults;
using MineCore.Utils;

namespace MineCore.Entities.Impl
{
    public partial class ServerPlayer
    {
        public void HandleDataPacket(DataPacket packet)
        {
            packet.ThrownOnArgNull(nameof(packet));

            if (packet is LoginPacket loginPacket)
            {
                OnLoginPacket(loginPacket);
            }
            else if (packet is ServerToClientHandshakePacket serverToClient)
            {
                OnServerToClientHandshakePacket(serverToClient);
            }
            else if (packet is ClientToServerHandshakePacket clientToServer)
            {
                OnClientToServerHandshakePacket(clientToServer);
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

            if (ServerListData.JoinedPlayer > ServerListData.MaxPlayer)
            {
                pk.Status = PlayStatusPacket.LOGIN_FAILED_SERVER_FULL;
                SendDataPacket(pk);
                return;
            }

            if (ServerConfig.UseXboxLiveAuth && !packet.TokenValidated)
            {
                DisconnectPacket pk2 = new DisconnectPacket();
                pk2.Message = "TokenValidateError";
                SendDataPacket(pk2);
                return;
            }

            LoginData = packet.LoginData;
            ClientData = packet.ClientData;

            if (ServerConfig.UseEncryption)
            {
                // TODO: Generate Jwt
                ServerToClientHandshakePacket handshake = new ServerToClientHandshakePacket();
                handshake.Token = "";
                SendDataPacket(handshake);
            }
            else
            {
                LoginSuccess();
            }
        }

        private void LoginSuccess()
        {
            PlayStatusPacket pk = new PlayStatusPacket();
            pk.Status = PlayStatusPacket.LOGIN_SUCCESS;
            SendDataPacket(pk);

            ResourcePacksInfoPacket pk2 = new ResourcePacksInfoPacket();
            SendDataPacket(pk2);
        }

        public void OnServerToClientHandshakePacket(ServerToClientHandshakePacket packet)
        {
            throw new NotImplementedException();
        }

        public void OnClientToServerHandshakePacket(ClientToServerHandshakePacket packet)
        {
            LoginSuccess();
        }
    }
}