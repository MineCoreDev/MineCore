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

        public bool TokenValidated { get; set; } = true;

        public override void EncodePayload()
        {
            WriteInt(Protocol);

            using (BinaryStream stream = new BinaryStream())
            {
                byte[] loginData = WriteLoginData();
                stream.WriteInt(loginData.Length, ByteOrder.Little);
                stream.WriteBytes(loginData);

                byte[] clientData = WriteClientData();
                stream.WriteInt(clientData.Length, ByteOrder.Little);
                stream.WriteBytes(clientData);

                WriteVarInt((int) stream.Length);
                WriteBytes(stream.ReadBytes());
            }
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

                WriteBytes(stream.GetBuffer());
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

                try
                {
                    JObject jwt = JObject.Parse(JWT.Decode(token.ToString(), ECDsa.Create(signParam)));
                    if (jwt.ContainsKey("extraData"))
                    {
                        JObject extData = JObject.Parse(jwt["extraData"].ToString());
                        LoginData.ClientUUID = new Guid(extData["identity"].Value<string>());
                        LoginData.DisplayName = extData["displayName"].Value<string>();
                        LoginData.IdentityPublicKey = jwt["identityPublicKey"].ToString();
                        LoginData.Xuid = extData["XUID"].Value<string>();
                    }
                }
                catch (IntegrityException)
                {
                    TokenValidated = false;

                    JObject jwt = JObject.Parse(JWT.Payload(token.ToString()));
                    if (jwt.ContainsKey("extraData"))
                    {
                        JObject extData = JObject.Parse(jwt["extraData"].ToString());
                        LoginData.ClientUUID = new Guid(extData["identity"].Value<string>());
                        LoginData.DisplayName = extData["displayName"].Value<string>();
                        LoginData.IdentityPublicKey = jwt["identityPublicKey"].ToString();
                        LoginData.Xuid = extData["XUID"].Value<string>();
                    }
                }
            }
        }

        public byte[] WriteLoginData()
        {
            throw new NotImplementedException();
        }

        public void ReadClientData(byte[] payload)
        {
            string json = Encoding.UTF8.GetString(payload);
            JObject data = JObject.Parse(JWT.Payload(json));

            ClientData.ClientRandomId = data["ClientRandomId"].Value<string>();
            ClientData.CurrentInputMode = data.Value<int>("CurrentInputMode");
            ClientData.DefaultInputMode = data.Value<int>("DefaultInputMode");
            ClientData.DeviceModel = data.Value<string>("DeviceModel");
            ClientData.DeviceOS = data.Value<int>("DeviceOS");
            ClientData.GameVersion = data.Value<string>("GameVersion");
            ClientData.GUIScale = data.Value<int>("GuiScale");
            ClientData.LanguageCode = data.Value<string>("LanguageCode");
            ClientData.ServerAddress = data.Value<string>("ServerAddress");
            ClientData.Skin = new Skin()
            {
                CapeData = Convert.FromBase64String(data.Value<string>("CapeData")),
                GeometryData = Encoding.UTF8.GetString(Convert.FromBase64String(data.Value<string>("SkinGeometry"))),
                GeometryName = data.Value<string>("SkinGeometryName"),
                SkinData = Convert.FromBase64String(data.Value<string>("SkinData")),
                SkinId = data.Value<string>("SkinId")
            };
            ClientData.UIProfile = data.Value<int>("UIProfile");
        }

        public byte[] WriteClientData()
        {
            throw new NotImplementedException();
        }
    }
}