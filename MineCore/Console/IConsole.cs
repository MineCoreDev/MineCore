using System;

namespace MineCore.Console
{
    public interface IConsole : IDisposable
    {
        void Start();
        void ProcessCommand(string cmd);
    }
}