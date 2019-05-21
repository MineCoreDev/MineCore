using MineCore.Entities.Attributes;

namespace MineCore.Entities
{
    public interface IEntityLiving : IEntityAttackable
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