using System;
using MineCore.Datas;
using MineCore.Events.Extensions;

namespace MineCore.Extensions
{
    public interface IExtensionPackageLoader : IName
    {
        event EventHandler<ExtensionPackageLoadEventArgs> LoadPackageEvent;
        event EventHandler<ExtensionPackageUnloadEventArgs> UnloadPackageEvent;

        void LoadPackages();
        void UnloadPackages();

        IExtensionPackage GetPackage(Type type);
        IExtensionPackage GetPackage(Guid guid);

        bool LoadPackage(Type type);
        bool LoadPackage(Guid type);

        bool UnloadPackage(Type type);
        bool UnloadPackage(Guid guid);
    }
}