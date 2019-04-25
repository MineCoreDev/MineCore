using MineCore.Datas;

namespace MineCore.ComponentModel
{
    public interface IComponent : IName
    {
        string NameSpace { get; }
        
        IComponentProperty<T> GetProperty<T>(string name);
        void SetProperty<T>(string name, IComponentProperty<T> property);
    }
}