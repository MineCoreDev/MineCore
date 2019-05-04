using System.Diagnostics;
using System.Threading;
using MineCore.Console;
using MineCore.Console.Impl;
using MineCore.Languages;
using MineCore.Net.Impl;
using NLog;

namespace MineCore.Platforms.Impl
{
    public class Platform : IPlatform
    {
        public Logger PlatformLogger => LogManager.GetCurrentClassLogger();

        public IConsole Console { get; private set; }

        public PlatformState State { get; protected set; } = PlatformState.Initialize;

        protected virtual bool Init()
        {
            try
            {
                StringManager.Init();
                Console = new MineCoreConsole();

                Thread.CurrentThread.Name = StringManager.GetString("minecore.thread.main");

                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual PlatformStartResult Start()
        {
            if (State == PlatformState.Started)
                return PlatformStartResult.Started;

            if (Init())
            {
                Console.Start();

                State = PlatformState.Started;

                return PlatformStartResult.Success;
            }

            State = PlatformState.ErrorExited;

            return PlatformStartResult.Failed;
        }

        public virtual PlatformStopResult Stop()
        {
            try
            {
                if (State == PlatformState.Started)
                {
                    Console.Dispose();

                    State = PlatformState.Stopped;

                    return PlatformStopResult.Success;
                }

                return PlatformStopResult.Stopped;
            }
            catch
            {
                State = PlatformState.ErrorExited;

                return PlatformStopResult.Failed;
            }
        }

        public virtual void WaitForStop()
        {
            while (State == PlatformState.Started)
            {
                Update();
                Thread.Sleep(1);
            }

            State = PlatformState.Exited;
        }

        public virtual void WaitForStop(long timeout)
        {
            Stopwatch watch = Stopwatch.StartNew();
            while (State == PlatformState.Started)
            {
                if (timeout < watch.Elapsed.TotalMilliseconds)
                {
                    break;
                }

                Update();
                Thread.Sleep(1);
            }

            State = PlatformState.Exited;
        }

        protected virtual void Update()
        {
        }
    }
}