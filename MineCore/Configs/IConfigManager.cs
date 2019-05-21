using System;

namespace MineCore.Configs
{
    public interface IConfigManager
    {
        IConfig LoadConfig(string path);
        T LoadConfig<T>(string path) where T : IConfig;

        bool SaveConfig(IConfig config);
        bool SaveConfig(string path, IConfig config);

        bool BackupConfig(IConfig config);

        IConfig GetConfig(Type type);
        IConfig GetConfig<T>();
    }
}