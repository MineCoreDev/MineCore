using Optional;

namespace MineCore.ComponentModel
{
    public interface IComponentProperty<T>
    {
        Option<T> Value { get; set; }
    }
}