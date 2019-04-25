using MineCore.Extensions;

namespace MineCore.Events.Extensions
{
    public class ExtensionPackageLoaderEventArgs : CancelableEventArgs
    {
        public IExtensionPackageLoader Loader { get; }

        public ExtensionPackageLoaderEventArgs(IExtensionPackageLoader loader)
        {
            Loader = loader;
        }
    }
}