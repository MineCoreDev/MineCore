using MineCore.Entities.Attributes;
using MineCore.Inventories;

namespace MineCore.Entities
{
    public interface IEntityLiving : IEntityAttackable, IEntityContainer
    {
        float Health { get; set; }
        float AbsorptionAmount { get; set; }

        short HurtTime { get; }
        int HurtByTimestamp { get; }
        short DeathTime { get; }

        int Age { get; }

        IEntityAttributeDictionary Attributes { get; set; }
    }
}