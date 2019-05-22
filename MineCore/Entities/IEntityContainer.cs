using MineCore.Inventories;

namespace MineCore.Entities
{
    public interface IEntityContainer
    {
        IEntityInventory Inventory { get; }
    }
}