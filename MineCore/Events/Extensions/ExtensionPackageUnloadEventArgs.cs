using MineCore.Extensions;
using MineCore.Utils;

namespace MineCore.Events.Extensions
{
    public class ExtensionPackageUnloadEventArgs : ExtensionPackageLoaderEventArgs
    {
        public IExtensionPackage Package { get; }

        public ExtensionPackageUnloadEventArgs(IExtensionPackageLoader loader, IExtensionPackage package) : base(loader)
        {
            package.ThrownOnArgNull(nameof(package));

            Package = package;
        }
    }
}