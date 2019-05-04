using System;
using System.Threading;
using System.Threading.Tasks;
using MineCore.Console.Logging.Impl;
using MineCore.Languages;
using MineCore.Utils;
using NLog;

namespace MineCore.Console.Impl
{
    public class MineCoreConsole : IConsole
    {
        internal int InputStartTop { get; set; } = 0;

        private Logger _logger = LogManager.GetCurrentClassLogger();

        private CancellationTokenSource _cancellationToken;
        private Task _worker;

        public MineCoreConsole()
        {
            System.Console.Title = StringManager.GetString("minecore.app.name");
            System.Console.CancelKeyPress += Console_CancelKeyPress;

            NLogConfigBuilder builder = new NLogConfigBuilder();
            LogManager.Configuration = builder.GetConfiguration(new MineCoreLoggerConfig());
            builder.ConsoleTarget.Console = this;
        }

        public void Start()
        {
            _cancellationToken = new CancellationTokenSource();
            _worker = Task.Factory.StartNew(() =>
            {
                Thread.CurrentThread.Name = StringManager.GetString("minecore.thread.console");

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

        private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _logger.Info(StringManager.GetString("minecore.app.stopping"));

            //TODO Request Stop Signal...
        }
    }
}