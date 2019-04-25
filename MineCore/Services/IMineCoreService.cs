using System;
using MineCore.Datas;
using MineCore.LifeCycles;

namespace MineCore.Services
{
    public interface IMineCoreService : IName, IDescription, ILoadable, IUnloadable, IEnable, IDisable
    {
        Type[] Dependencies { get; }
    }
}