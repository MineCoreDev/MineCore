using System;
using MineCore.Config;
using MineCore.Console;
using NLog;

namespace MineCore.Platforms
{
    public interface IPlatform
    {
        Logger PlatformLogger { get; }

        IPlatformConfig Config { get; }

        IConsole Console { get; }

        PlatformState State { get; }

        PlatformStartResult Start();

        PlatformStopResult Stop();

        void WaitForStop();
        void WaitForStop(long timeout);
    }
}