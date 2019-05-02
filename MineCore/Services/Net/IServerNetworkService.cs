using MineCore.Net;

namespace MineCore.Services.Net
{
    public interface IServerNetworkService : IMineCoreService
    {
        IMineCraftProtocol Protocol { get; }
        IServerListData ListData { get; }
        IServerNetworkManager Manager { get; }
    }
}