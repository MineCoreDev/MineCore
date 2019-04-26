using MineCore.Services;
using MineCore.Utils;

namespace MineCore.Events.Services
{
    public class ServiceContainerEventArgs : CancelableEventArgs
    {
        public IServiceContainer Container { get; }

        public ServiceContainerEventArgs(IServiceContainer container)
        {
            container.ThrownOnArgNull(nameof(container));

            Container = container;
        }
    }
}