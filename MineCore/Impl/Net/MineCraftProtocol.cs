using MineCore.Net;

namespace MineCore.Impl.Net
{
    public class MineCraftProtocol : IMineCraftProtocol
    {
        public int ProtocolNumber => 354;

        public string Version => "1.11.1";
    }
}