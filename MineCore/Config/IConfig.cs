using System;

namespace MineCore.Config
{
    public interface IConfig
    {
        Version ConfigVersion { get; set; }

        string FileName { get; }

        ConfigSaveResult Save();
    }
}