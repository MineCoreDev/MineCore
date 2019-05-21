using MineCore.Worlds;

namespace MineCore.Values
{
    public interface IBlockPosition2d : IVector2Int
    {
        IWorld World { get; set; }
    }
}