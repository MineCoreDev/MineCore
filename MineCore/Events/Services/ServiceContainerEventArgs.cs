using MineCore.Services;

namespace MineCore.Events.Services
{
    public class ServiceContainerEventArgs : CancelableEventArgs
    {
        public IServiceContainer Container { get; }

        public ServiceContainerEventArgs(IServiceContainer container)
        {
            Container = container;
        }
    }
}