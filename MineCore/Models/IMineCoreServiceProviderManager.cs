using System;
using MineCore.Events;

namespace MineCore.Models
{
    public interface IMineCoreServiceProviderManager
    {
        IMineCoreServiceProvider[] ServiceProviders { get; }
        string ExternalServiceFolderPath { get; }

        event EventHandler<ServiceProviderManagerEventArgs> LoadInternalServices;
        event EventHandler<ServiceProviderManagerEventArgs> UnLoadInternalServices;
        event EventHandler<ServiceProviderManagerEventArgs> LoadExternalServices;
        event EventHandler<ServiceProviderManagerEventArgs> UnLoadExternalServices;


        void LoadingInternalServices();
        void LoadingExternalServices();

        void UnloadingInternalServices();
        void UnloadingExternalServices();
    }
}