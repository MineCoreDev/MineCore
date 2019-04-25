using System;
using System.Reflection;
using MineCore.Datas;
using MineCore.LifeCycles;

namespace MineCore.Extensions
{
    public interface IExtensionPackage : IName, IDescription, ILoadable, IUnloadable
    {
        Type[] Dependencies { get; }

        Assembly Assembly { get; }

        Version Version { get; }
        Version ApiVersion { get; }
    }
}