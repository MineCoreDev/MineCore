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
            server.PlatformLogger.Info("a" + Environment.NewLine + "b" + Environment.NewLine + "c" +
                                       Environment.NewLine + "e" + Environment.NewLine);
            server.WaitForStop();
        }
    }
}