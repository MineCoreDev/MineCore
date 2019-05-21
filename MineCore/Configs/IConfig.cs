namespace MineCore.Configs
{
    public interface IConfig
    {
        string GetFilePath();

        bool Save();
        bool Backup();
    }
}