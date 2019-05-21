using MineCore.Entities.Events;

namespace MineCore.Entities
{
    public interface IEntityAttackable : IEntity
    {
        IEntityDamageEvent LastDamageEvent { get; }

        bool Attack(int damageCause, float damage);
        bool Attack(IEntityDamageEvent damageEvent);
    }
}