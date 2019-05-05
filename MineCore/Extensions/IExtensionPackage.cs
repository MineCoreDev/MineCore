using System;
using System.Reflection;
using MineCore.Data;
using MineCore.LifeCycles;
using NLog;

namespace MineCore.Extensions
{
    public interface IExtensionPackage : IName, IDescription, ILoadable, IUnloadable
    {
        Logger PackageLogger { get; }

        Assembly Assembly { get; }

        Version Version { get; }
        Version ApiVersion { get; }
    }
}