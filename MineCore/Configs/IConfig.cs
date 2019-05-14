using YamlDotNet.Serialization;

namespace MineCore.Configs
{
    public interface IConfig
    {
        [YamlIgnore] string FileName { get; }

        ConfigSaveResult Save();
    }
}