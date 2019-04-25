using System.Collections.Generic;
using MineCore.ComponentModel;

namespace MineCore.Datas.Components
{
    public class ComponentDefinitions
    {
        private Dictionary<string, IComponent> _defineComponents = new Dictionary<string, IComponent>();

        public void RegisterComponent(IComponent component)
        {
            string name = component.NameSpace + ":" + component.Name;
            if (!_defineComponents.ContainsKey(name))
            {
                _defineComponents.Add(name, component);
            }
        }
    }
}