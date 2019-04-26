using MineCore.Services;
using MineCore.Utils;

namespace MineCore.Events.Services
{
    public class ServiceLoadEventArgs : ServiceContainerEventArgs
    {
        public IMineCoreService Service { get; set; }

        public ServiceLoadEventArgs(IServiceContainer container, IMineCoreService service)
            : base(container)
        {
            service.ThrownOnArgNull(nameof(service));

            Service = service;
        }
    }
}