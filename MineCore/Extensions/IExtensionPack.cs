using System;

namespace MineCore.Extensions
{
    public interface IExtensionPack
    {
        Version ExtensionVersion { get; }
        IExtensionPackDependency[] Dependencies { get; }

        void OnLoadExtension();
        void OnEnableExtension();
        void OnUnloadExtension();
        void OnDisableExtension();
    }
}