using System;

namespace MineCore.Extensions
{
    public interface IExtensionManager
    {
        void LoadAllExtensions();
        void UnloadAllExtensions();

        void LoadExtensionPackage(string path);
        void LoadExtensionPackage(IExtensionPack pack);
        void UnloadExtensionPackage(Type type);
        void UnloadExtensionPackage(IExtensionPack pack);

        IExtensionPack GetExtensionPack(Type type);
    }
}