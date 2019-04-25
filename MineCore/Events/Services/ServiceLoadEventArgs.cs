using MineCore.Services;

namespace MineCore.Events.Services
{
    public class ServiceLoadEventArgs : ServiceContainerEventArgs
    {
        public IMineCoreService Service { get; set; }

        public ServiceLoadEventArgs(IServiceContainer container, IMineCoreService service)
            : base(container)
        {
            Service = service;
        }
    }
}