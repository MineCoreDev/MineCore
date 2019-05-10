using System;
using System.Net;

namespace MineCore.Entities
{
    public interface IPlayer : IHuman, IDisposable
    {
        IPEndPoint EndPoint { get; }
    }
}