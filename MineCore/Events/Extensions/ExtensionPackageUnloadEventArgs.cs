using MineCore.Extensions;

namespace MineCore.Events.Extensions
{
    public class ExtensionPackageUnloadEventArgs : ExtensionPackageLoaderEventArgs
    {
        public IExtensionPackage Package { get; }

        public ExtensionPackageUnloadEventArgs(IExtensionPackageLoader loader, IExtensionPackage package) : base(loader)
        {
            Package = package;
        }
    }
}