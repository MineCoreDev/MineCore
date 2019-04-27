using MineCore.Console;
using MineCore.Impl.Console;
using MineCore.Services;

namespace MineCore.Impl.Services.Console
{
    public abstract class ConsoleService : MineCoreService
    {
        public IConsole Console;
    }
}