using MineCore.Entities;

namespace MineCore.Blocks
{
    public interface IBlockContainer : IBlock
    {
        void Open(IPlayer player);
        void Close(IPlayer player);
    }
}