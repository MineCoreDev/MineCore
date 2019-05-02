using System;
using MineCore.Services;
using NLog;

namespace MineCore.Impl.Services
{
    public abstract class MineCoreService : IMineCoreService
    {
        public virtual string Name => GetType().FullName;
        public virtual string Description { get; }

        public Logger ServiceLogger { get; } = LogManager.GetCurrentClassLogger();
        public virtual Type[] Dependencies { get; } = new Type[0];

        public virtual void OnLoad()
        {
        }

        public virtual void OnUnload()
        {
        }

        public virtual void OnEnable()
        {
        }

        public virtual void OnDisable()
        {
        }
    }
}