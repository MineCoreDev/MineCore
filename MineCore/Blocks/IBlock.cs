using MineCore.Data;

namespace MineCore.Blocks
{
    public interface IBlock : IName
    {
        int BlockId { get; }

        IBlockState[] BlockStateList { get; }
        IBlockState BlockState { get; }
    }
}