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
        public const byte TEXT_PACKET = 0x09;
        public const byte SET_TIME_PACKET = 0x0a;
        public const byte START_GAME_PACKET = 0x0b;
        public const byte ADD_PLAYER_PACKET = 0x0c;
        public const byte ADD_ENTITY_PACKET = 0x0d;
        public const byte REMOVE_ENTITY_PACKET = 0x0e;
        public const byte ADD_ITEM_ENTITY_PACKET = 0x0f;

        public const byte FULL_CHUNK_DATA_PACKET = 0x3a;

        public const byte REQUEST_CHUNK_RADIUS_PACKET = 0x45;
        public const byte CHUNK_RADIUS_UPDATED_PACKET = 0x46;

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
            RegisterDataPacket(RESOURCE_PACK_STACK_PACKET, typeof(ResourcePackStackPacket));
            RegisterDataPacket(RESOURCE_PACK_CLIENT_RESPONSE_PACKET, typeof(ResourcePackClientResponsePacket));

            RegisterDataPacket(REQUEST_CHUNK_RADIUS_PACKET, typeof(RequestChunkRadiusPacket));
            RegisterDataPacket(CHUNK_RADIUS_UPDATED_PACKET, typeof(ChunkRadiusUpdatedPacket));

            Compile();
        }
    }
}