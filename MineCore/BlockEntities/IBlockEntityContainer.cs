using MineCore.Inventories;

namespace MineCore.BlockEntities
{
    public interface IBlockEntityContainer : IBlockEntity
    {
        IInventory Inventory { get; }
    }
}