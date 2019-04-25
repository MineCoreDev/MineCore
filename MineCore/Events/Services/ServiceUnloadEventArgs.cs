using MineCore.Services;

namespace MineCore.Events.Services
{
    public class ServiceUnloadEventArgs : ServiceContainerEventArgs
    {
        public IMineCoreService Service { get; set; }
        
        public ServiceUnloadEventArgs(IServiceContainer container, IMineCoreService service)
            : base(container)
        {
            Service = service;
        }
    }
}