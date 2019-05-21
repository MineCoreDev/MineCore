using MineCore.Entities.Attributes;

namespace MineCore.Entities
{
    public interface IEntityLiving : IEntity
    {
        float Health { get; set; }
        float AbsorptionAmount { get; set; }

        short HurtTime { get; }
        int HurtByTimestamp { get; }
        short DeathTime { get; }

        IEntityAttributeDictionary Attributes { get; set; }
    }
}