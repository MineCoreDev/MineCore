using MineCore.Worlds;

namespace MineCore.Values
{
    public interface IBlockPosition3d : IVector3Int
    {
        IWorld World { get; set; }
    }
}