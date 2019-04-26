using System;
using System.Collections.Generic;
using System.Text;
using MineCore.Impl.Services;
using MineCore.Platforms;

namespace MineCore.Server
{
    public class ServerPlatform : ServiceContainer, IServerPlatform
    {
        public PlatformStartResult Start()
        {
            return PlatformStartResult.Start;
        }
    }
}