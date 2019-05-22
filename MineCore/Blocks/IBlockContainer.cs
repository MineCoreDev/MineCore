using MineCore.BlockEntities;
using MineCore.Entities;

namespace MineCore.Blocks
{
    public interface IBlockContainer : IBlock
    {
        IBlockEntityContainer BlockEntityContainer { get; }

        void Open(IPlayer player);
        void Close(IPlayer player);
    }
}