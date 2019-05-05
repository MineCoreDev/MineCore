using System;
using MineCore.Data;
using MineCore.Events.Extensions;
using NLog;
using Optional;

namespace MineCore.Extensions
{
    public interface IExtensionPackageLoader : IName
    {
        Logger LoaderLogger { get; }

        event EventHandler<ExtensionPackageLoadEventArgs> LoadPackageEvent;
        event EventHandler<ExtensionPackageUnloadEventArgs> UnloadPackageEvent;

        void LoadPackages();
        void UnloadPackages();

        Option<IExtensionPackage> GetPackage(Type type);
        Option<IExtensionPackage> GetPackage(Guid guid);

        bool LoadPackage(Type type);
        bool LoadPackage(Guid type);

        bool UnloadPackage(Type type);
        bool UnloadPackage(Guid guid);
    }
}