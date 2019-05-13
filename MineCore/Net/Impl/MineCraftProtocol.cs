using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using MineCore.Net.Protocols;
using MineCore.Net.Protocols.Defaults;
using MineCore.Utils;
using Optional;

namespace MineCore.Net.Impl
{
    public class MineCraftProtocol : IMineCraftProtocol
    {
        public const byte LOGIN_PACKET = 0x01;
        public const byte PLAY_STATUS_PACKET = 0x02;
        public const byte SERVER_TO_CLIENT_HANDSHAKE_PACKET = 0x03;
        public const byte CLIENT_TO_SERVER_HANDSHAKE_PACKET = 0x04;
        public const byte DISCONNECT_PACKET = 0x05;
        public const byte RESOURCE_PACKS_INFO_PACKET = 0x06;
        public const byte RESOURCE_PACK_STACK_PACKET = 0x07;
        public const byte RESOURCE_PACK_CLIENT_RESPONSE_PACKET = 0x08;

        public int ProtocolNumber => 354;

        public string Version => "1.11.1";

        private readonly ConcurrentDictionary<int, Type> _registeredPacket = new ConcurrentDictionary<int, Type>();

        private readonly ConcurrentDictionary<int, Func<DataPacket>> _packetCompiledPacket =
            new ConcurrentDictionary<int, Func<DataPacket>>();

        public MineCraftProtocol()
        {
            Init();
        }

        public void Compile()
        {
            foreach (var kv in _registeredPacket)
            {
                CompileFromId(kv.Key);
            }
        }

        public void CompileFromId(int id)
        {
            _packetCompiledPacket.TryAdd(id,
                Expression.Lambda<Func<DataPacket>>(Expression.New(_registeredPacket[id].GetConstructor(
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder,
                        new Type[0], null)))
                    .Compile());
        }

        public Option<DataPacket> GetDefinedPacket(byte id)
        {
            _packetCompiledPacket.TryGetValue(id, out var packet);
            if (packet == null)
                return Option.None<DataPacket>();

            return packet.Invoke().SomeNotNull();
        }

        public void RegisterDataPacket(int id, Type packetType, bool compile = false)
        {
            _registeredPacket.TryAdd(id, packetType);

            if (compile)
                CompileFromId(id);
        }

        private void Init()
        {
            RegisterDataPacket(LOGIN_PACKET, typeof(LoginPacket));
            RegisterDataPacket(PLAY_STATUS_PACKET, typeof(PlayStatusPacket));
            RegisterDataPacket(SERVER_TO_CLIENT_HANDSHAKE_PACKET, typeof(ServerToClientHandshakePacket));
            RegisterDataPacket(CLIENT_TO_SERVER_HANDSHAKE_PACKET, typeof(ClientToServerHandshakePacket));
            RegisterDataPacket(DISCONNECT_PACKET, typeof(DisconnectPacket));
            RegisterDataPacket(RESOURCE_PACKS_INFO_PACKET, typeof(ResourcePacksInfoPacket));

            Compile();
        }
    }
}