using MineCore.Utils;
using Optional;

namespace MineCore.ComponentModel
{
    public interface IComponentDefinitions
    {
        bool RegisterComponent(IComponent component);

        Option<IComponent> GetComponent(string name);

        Option<T> GetComponent<T>(string name) where T : IComponent;
    }
}