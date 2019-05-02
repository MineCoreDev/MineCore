using System;

namespace MineCore.Console
{
    public interface IConsole : IDisposable
    {
        void ProcessCommand(string cmd);
    }
}