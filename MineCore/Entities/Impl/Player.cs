using System;
using System.Net;
using MineCore.Net;

namespace MineCore.Entities.Impl
{
    public class Player : IPlayer, IPacketHandler
    {
        public IPEndPoint EndPoint { get; private set; }

        public long EntityId { get; }
        public long EntityRuntimeId { get; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}