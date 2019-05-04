using System;
using MineCore.Console;
using NLog;

namespace MineCore.Platforms
{
    public interface IPlatform
    {
        Logger PlatformLogger { get; }

        IConsole Console { get; }

        PlatformState State { get; }

        PlatformStartResult Start();

        PlatformStopResult Stop();

        void WaitForStop();
        void WaitForStop(long timeout);
    }
}