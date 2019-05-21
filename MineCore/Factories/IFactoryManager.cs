using System;

namespace MineCore.Factories
{
    public interface IFactoryManager
    {
        void AddFactory<K, V>(IFactory<K, V> factory);
        void RemoveFactory(Type type);

        IFactory<K, V> GetFactory<K, V>(Type type);
    }
}