using System;
using MineCore.Net;

namespace MineCore.Entities
{
    public interface IClientPlayer : IHuman, IDataPacketHandler, IDisposable
    {
    }
}