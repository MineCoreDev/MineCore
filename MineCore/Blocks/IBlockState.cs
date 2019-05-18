using MineCore.Data;

namespace MineCore.Blocks
{
    public interface IBlockState : IName
    {
        byte StateData { get; }
    }
}