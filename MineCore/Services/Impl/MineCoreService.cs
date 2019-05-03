using System;
using NLog;

namespace MineCore.Services.Impl
{
    public abstract class MineCoreService : IMineCoreService
    {
        public virtual string Name => GetType().FullName;
        public virtual string Description { get; }

        private IServiceContainer _container;

        public IServiceContainer Container
        {
            get => _container;
            set
            {
                if (_container == null)
                    _container = value;
            }
        }

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