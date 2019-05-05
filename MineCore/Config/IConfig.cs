using System;

namespace MineCore.Config
{
    public interface IConfig
    {
        Version ConfigVersion { get; }

        ConfigLoadResult Load();
        ConfigSaveResult Save();
    }
}