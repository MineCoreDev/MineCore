using MineCore.Impl.Console;

namespace MineCore.Impl.Services.Console
{
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