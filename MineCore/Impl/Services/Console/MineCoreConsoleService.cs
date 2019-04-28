using MineCore.Impl.Console;
using MineCore.Services;

namespace MineCore.Impl.Services.Console
{
    [ServerPlatformService, ClientPlatformService]
    public class MineCoreConsoleService : ConsoleService
    {
        public override void OnEnable()
        {
            MineCoreConsole console = new MineCoreConsole();
            Console = console;
            console.StartWorker();
        }
    }
}