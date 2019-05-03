using MineCore.Console;
using NLog;

namespace MineCore.Platforms
{
    public interface IPlatform
    {
        Logger PlatformLogger { get; }

        IConsole Console { get; }

        PlatformStartResult Start();
    }
}