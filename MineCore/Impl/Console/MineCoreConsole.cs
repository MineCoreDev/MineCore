using System;
using System.Threading;
using System.Threading.Tasks;
using MineCore.Console;
using MineCore.Languages;

namespace MineCore.Impl.Console
{
    public class MineCoreConsole : IConsole
    {
        public CancellationTokenSource CancellationToken { get; private set; }

        public Task Worker { get; private set; }

        public MineCoreConsole()
        {
            System.Console.Title = StringManager.GetString("minecore.app.name");
        }

        public void StartWorker()
        {
            CancellationToken = new CancellationTokenSource();
            Worker = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (CancellationToken.Token.IsCancellationRequested)
                    {
                        break;
                    }

                    ProcessCommand(ReadLine.Read(">"));
                }
            }, CancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void ProcessCommand(string cmd)
        {
        }
    }
}