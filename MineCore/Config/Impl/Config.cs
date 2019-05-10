﻿using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace MineCore.Config.Impl
{
    public class Config : IConfig
    {
        [JsonIgnore] public virtual string FileName => "";

        public static (ConfigLoadResult result, T config) Load<T>(string fileName, bool currentFile = true)
            where T : Config
        {
            if (currentFile)
                fileName = Environment.CurrentDirectory + Path.DirectorySeparatorChar + fileName;

            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName, Encoding.UTF8);
                try
                {
                    return (ConfigLoadResult.Success, JsonConvert.DeserializeObject<T>(json,
                        new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.All
                        }));
                }
                catch
                {
                    File.Copy(fileName, fileName + ".old", true);
                    T data = Activator.CreateInstance<T>();
                    data.Save();
                    return (ConfigLoadResult.Upgrade, data);
                }
            }
            else
            {
                T data = Activator.CreateInstance<T>();
                data.Save();
                return (ConfigLoadResult.Success, data);
            }
        }

        public virtual ConfigSaveResult Save()
        {
            try
            {
                string fullName = Environment.CurrentDirectory + Path.DirectorySeparatorChar + FileName;
                string json = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                });
                File.WriteAllText(fullName, json, Encoding.UTF8);

                return ConfigSaveResult.Success;
            }
            catch
            {
                return ConfigSaveResult.Failed;
            }
        }
    }
}