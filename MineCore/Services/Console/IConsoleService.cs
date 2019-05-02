using MineCore.Console;
using MineCore.Impl.Console;
using MineCore.Services;

namespace MineCore.Impl.Services.Console
{
    public interface IConsoleService : IMineCoreService
    {
        IConsole Console { get; }
    }
}