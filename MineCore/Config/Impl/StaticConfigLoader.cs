using System;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace MineCore.Config.Impl
{
    public class StaticConfigLoader
    {
        public static (ConfigLoadResult result, T config) Load<T>(string fileName) where T : IConfig, new()
        {
            if (File.Exists(fileName))
            {
                try
                {
                    string yaml = File.ReadAllText(fileName, Encoding.UTF8);
                    Deserializer db = (Deserializer) new DeserializerBuilder().Build();
                    return (ConfigLoadResult.Success, db.Deserialize<T>(yaml));
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                    return (ConfigLoadResult.Upgrade, new T());
                }
            }
            else
            {
                T data = new T();
                data.Save();
                return (ConfigLoadResult.Success, data);
            }
        }
    }
}