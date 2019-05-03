using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MineCore.Console;
using MineCore.Impl.Console.Logging;
using MineCore.Languages;
using MineCore.Utils;
using NLog;

namespace MineCore.Impl.Console
{
    public class MineCoreConsole : IConsole
    {
        internal int InputStartTop { get; set; } = 0;

        private CancellationTokenSource _cancellationToken;
        private Task _worker;

        public MineCoreConsole()
        {
            System.Console.Title = StringManager.GetString("minecore.app.name");

            NLogConfigBuilder builder = new NLogConfigBuilder();
            LogManager.Configuration = builder.GetConfiguration(new MineCoreLoggerConfig());
            builder.ConsoleTarget.Console = this;
        }

        public void StartWorker()
        {
            _cancellationToken = new CancellationTokenSource();
            _worker = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (_cancellationToken.Token.IsCancellationRequested)
                    {
                        break;
                    }

                    InputStartTop = System.Console.CursorTop;
                    ProcessCommand(ReadLine.Read("> "));
                }
            }, _cancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void ProcessCommand(string cmd)
        {
            cmd.ThrownOnArgNull(nameof(cmd));
        }

        public void Dispose()
        {
            if (_worker != null)
            {
                _cancellationToken.Cancel();
                _cancellationToken = null;
                _worker.Dispose();
                _worker = null;
            }
        }
    }
}