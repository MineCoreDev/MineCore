using System;
using MineCore.Data;
using MineCore.Net;

namespace MineCore.Entities
{
    public interface IPlayer : IHuman, IDisposable
    {
        IMineCraftProtocol Protocol { get; }

        ILoginData LoginData { get; }
        IClientData ClientData { get; }

        bool IsEncrypt { get; }

        void Close(string message);
    }
}