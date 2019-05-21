using System;

namespace MineCore.Factories
{
    public interface IFactory<K, V>
    {
        void Register(K key, Type type);
        void RegisterOverride(K key, Type type);

        void CompileAll();
        void Compile(K key);

        V Get(K key);
    }
}