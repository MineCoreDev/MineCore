using System;
using System.Threading;
using System.Threading.Tasks;

namespace MineCore.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerPlatform server = new ServerPlatform();
            server.Start();
        }
    }
}