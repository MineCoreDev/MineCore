using System;
using System.Net;
using MineCore.Net;

namespace MineCore.Entities
{
    public interface IPlayer : IHuman, IDataPacketHandler, IDisposable
    {
        IPEndPoint EndPoint { get; }
    }
}