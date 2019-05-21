using MineCore.Worlds;

namespace MineCore.Values
{
    public interface IPosition : IVector3
    {
        IWorld World { get; set; }
    }
}