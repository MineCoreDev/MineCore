using MineCore.Data;
using Optional;

namespace MineCore.ComponentModel
{
    public interface IComponent : IName
    {
        string NameSpace { get; }

        Option<IComponentProperty<T>> GetProperty<T>(string name);
        void SetProperty<T>(string name, IComponentProperty<T> property);
    }
}