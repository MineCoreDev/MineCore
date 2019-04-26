using MineCore.Extensions;
using MineCore.Utils;

namespace MineCore.Events.Extensions
{
    public class ExtensionPackageLoaderEventArgs : CancelableEventArgs
    {
        public IExtensionPackageLoader Loader { get; }

        public ExtensionPackageLoaderEventArgs(IExtensionPackageLoader loader)
        {
            loader.ThrownOnArgNull(nameof(loader));

            Loader = loader;
        }
    }
}