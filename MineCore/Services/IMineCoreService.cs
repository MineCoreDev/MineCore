using System;
using MineCore.Datas;
using MineCore.LifeCycles;
using NLog;

namespace MineCore.Services
{
    public interface IMineCoreService : IName, IDescription, ILoadable, IUnloadable, IEnable, IDisable
    {
        IServiceContainer Container { get; set; }

        Logger ServiceLogger { get; }

        Type[] Dependencies { get; }
    }
}