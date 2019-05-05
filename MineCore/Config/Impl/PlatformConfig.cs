using System;
using System.IO;
using System.Text;
using MineCore.Console.Logging;
using MineCore.Console.Logging.Impl;
using YamlDotNet.Serialization;

namespace MineCore.Config.Impl
{
    public class PlatformConfig : StaticConfigLoader, IPlatfromConfig
    {
        public Version ConfigVersion { get; set; } = new Version(1, 0, 0, 0);

        [YamlIgnore]
        public string FileName => Environment.CurrentDirectory + Path.DirectorySeparatorChar + "config.yml";

        public ILoggerConfig LoggerConfig { get; set; } = new MineCoreLoggerConfig();

        public ConfigSaveResult Save()
        {
            try
            {
                SerializerBuilder sb = new SerializerBuilder()
                    .EmitDefaults();
                Serializer s = (Serializer) sb.Build();
                string yaml = s.Serialize(this);
                File.WriteAllText(FileName, yaml, Encoding.UTF8);

                return ConfigSaveResult.Success;
            }
            catch
            {
                return ConfigSaveResult.Failed;
            }
        }
    }
}