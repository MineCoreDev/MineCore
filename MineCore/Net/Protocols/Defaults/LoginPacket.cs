using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BinaryIO;
using Jose;
using MineCore.Data.Impl;
using MineCore.Languages;
using MineCore.Net.Impl;
using MineCore.Utils;
using Newtonsoft.Json.Linq;
using NLog;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

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
            LoginData = new LoginData();
            ClientData = new ClientData();

            Protocol = ReadInt();

            int len = ReadVarInt();
            using (BinaryStream stream = new BinaryStream(ReadBytes(len)))
            {
                int loginDataLen = stream.ReadInt(ByteOrder.Little);
                ReadLoginData(stream.ReadBytes(loginDataLen));
                int clientDataLen = stream.ReadInt(ByteOrder.Little);
                ReadClientData(stream.ReadBytes(clientDataLen));
            }
        }

        public void ReadLoginData(byte[] payload)
        {
            string json = Encoding.UTF8.GetString(payload);
            JObject data = JObject.Parse(json);
            JToken chainToken = data["chain"];
            foreach (var token in chainToken)
            {
                IDictionary<string, object> headers = JWT.Headers(token.ToString());
                string x5u = headers["x5u"].ToString();
                ECPublicKeyParameters x5KeyParam =
                    (ECPublicKeyParameters) PublicKeyFactory.CreateKey(Convert.FromBase64String(x5u));
                ECParameters signParam = new ECParameters
                {
                    Curve = ECCurve.NamedCurves.nistP384,
                    Q =
                    {
                        X = x5KeyParam.Q.AffineXCoord.GetEncoded(),
                        Y = x5KeyParam.Q.AffineYCoord.GetEncoded()
                    },
                };
                signParam.Validate();


                JObject jwt = JObject.Parse(JWT.Decode(token.ToString(), ECDsa.Create(signParam)));
                if (jwt.ContainsKey("extraData"))
                {
                    JObject extData = JObject.Parse(jwt["extraData"].ToString());
                    LoginData.ClientUUID = new Guid(extData["identity"].Value<string>());
                    LoginData.DisplayName = extData["displayName"].Value<string>();
                    LoginData.IdentityPublicKey = jwt["identityPublicKey"].ToString();
                    LoginData.Xuid = extData["XUID"].Value<string>();
                }

                LogManager.GetCurrentClassLogger().Info(LoginData.DisplayName);
            }
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