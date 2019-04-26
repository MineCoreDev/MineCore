using System;
using System.Reflection;
using MineCore.Datas;
using MineCore.LifeCycles;
using NLog;

namespace MineCore.Extensions
{
    public interface IExtensionPackage : IName, IDescription, ILoadable, IUnloadable
    {
        Logger PackageLogger { get; }

        Type[] Dependencies { get; }

        Assembly Assembly { get; }

        Version Version { get; }
        Version ApiVersion { get; }
    }
}