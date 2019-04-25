using MineCore.Extensions;

namespace MineCore.Events.Extensions
{
    public class ExtensionPackageLoadEventArgs : ExtensionPackageLoaderEventArgs
    {
        public IExtensionPackage Package { get; set; }

        public ExtensionPackageLoadEventArgs(IExtensionPackageLoader loader, IExtensionPackage package)
            : base(loader)
        {
            Package = package;
        }
    }
}