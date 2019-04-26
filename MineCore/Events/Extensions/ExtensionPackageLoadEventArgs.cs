using MineCore.Extensions;
using MineCore.Utils;

namespace MineCore.Events.Extensions
{
    public class ExtensionPackageLoadEventArgs : ExtensionPackageLoaderEventArgs
    {
        public IExtensionPackage Package { get; set; }

        public ExtensionPackageLoadEventArgs(IExtensionPackageLoader loader, IExtensionPackage package)
            : base(loader)
        {
            package.ThrownOnArgNull(nameof(package));

            Package = package;
        }
    }
}