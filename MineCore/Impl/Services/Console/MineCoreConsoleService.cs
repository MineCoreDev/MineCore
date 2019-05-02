using MineCore.Console;
using MineCore.Impl.Console;
using MineCore.Services;

namespace MineCore.Impl.Services.Console
{
    [ServerPlatformService, ClientPlatformService]
    public class MineCoreConsoleService : MineCoreService, IConsoleService
    {
        public IConsole Console { get; private set; }

        public override void OnEnable()
        {
            MineCoreConsole console = new MineCoreConsole();
            Console = console;
            console.StartWorker();
        }

        public override void OnDisable()
        {
            Console.Dispose();
        }
    }
}