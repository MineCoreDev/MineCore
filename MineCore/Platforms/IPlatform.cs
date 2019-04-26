using NLog;

namespace MineCore.Platforms
{
    public interface IPlatform
    {
        Logger PlatformLogger { get; }

        PlatformStartResult Start();
    }
}