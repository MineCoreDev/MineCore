using System;
using System.Threading;
using System.Threading.Tasks;
using MineCore.Console;
using MineCore.Languages;
using MineCore.Utils;

namespace MineCore.Impl.Console
{
    public class MineCoreConsole : IConsole
    {
        private CancellationTokenSource _cancellationToken;
        private Task _worker;

        public MineCoreConsole()
        {
            System.Console.Title = StringManager.GetString("minecore.app.name");
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