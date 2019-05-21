using System;

namespace MineCore.Extensions
{
    public interface IExtensionPackDependency
    {
        Version DependencyVersion { get; }
        Guid DependencyPackageGuid { get; }
    }
}