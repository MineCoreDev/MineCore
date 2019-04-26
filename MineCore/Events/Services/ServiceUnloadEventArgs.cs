using MineCore.Services;
using MineCore.Utils;

namespace MineCore.Events.Services
{
    public class ServiceUnloadEventArgs : ServiceContainerEventArgs
    {
        public IMineCoreService Service { get; set; }

        public ServiceUnloadEventArgs(IServiceContainer container, IMineCoreService service)
            : base(container)
        {
            service.ThrownOnArgNull(nameof(service));

            Service = service;
        }
    }
}