using System.Collections.Generic;

namespace MineCore
{
    public interface IMineCoreServiceProviderManager
    {
        List<IMineCoreServiceProvider> Services { get; }

        void LoadInternalServices();
        void LoadExternalServices();
        
        void UnloadInternalServices();
        void UnloadExternalServices();
    }
}