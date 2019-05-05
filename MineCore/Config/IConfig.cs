using System;
using YamlDotNet.Serialization;

namespace MineCore.Config
{
    public interface IConfig
    {
        [YamlIgnore] string FileName { get; }

        ConfigSaveResult Save();
    }
}