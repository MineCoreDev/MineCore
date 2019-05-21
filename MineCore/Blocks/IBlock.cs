using MineCore.Entities;
using MineCore.Items;
using MineCore.Values;

namespace MineCore.Blocks
{
    public interface IBlock : IBlockPosition3d
    {
        string Name { get; }
        int Id { get; }

        IBlockState BlockState { get; }

        bool IsSolid { get; }
        bool IsTransparent { get; }
        bool IsWall { get; }
        bool IsPanel { get; }

        bool CanPassThrough { get; }
        bool CanBePlace { get; }
        bool CanBeBreak { get; }
        bool CanBeActivate { get; }

        IItem ItemBlock { get; }

        void OnUpdate(int type);

        IBlock GetSideBlock(BlockFace face);

        bool OnPlace(IBlock clickBlock, IBlock replace, BlockFace face, IPlayer player, IItem item);
        bool OnBreak(BlockFace face, IPlayer player, IItem item);
        bool OnActivate(BlockFace face, IPlayer player, IItem item);

        IItem[] GetDrops();
    }
}