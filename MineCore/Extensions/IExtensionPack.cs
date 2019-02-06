using System;

namespace MineCore.Extensions
{
    public interface IExtensionPack
    {   
        string Name { get; }
        string Description { get; }
        string DataFolderPath { get; }
        Version Version { get; }
        
        Type[] Dependencies { get; }
        
        ExtensionFlags Flag { get; }
    }
}