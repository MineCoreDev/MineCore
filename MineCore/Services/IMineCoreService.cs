using System;
using MineCore.Datas;
using MineCore.LifeCycles;
using NLog;

namespace MineCore.Services
{
    public interface IMineCoreService : IName, IDescription, ILoadable, IUnloadable, IEnable, IDisable
    {
        Logger ServiceLogger { get; }

        Type[] Dependencies { get; }
    }
}