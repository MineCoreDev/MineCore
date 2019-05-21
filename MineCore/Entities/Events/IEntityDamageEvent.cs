namespace MineCore.Entities.Events
{
    public interface IEntityDamageEvent
    {
        int DamageCause { get; }
        float Damage { get; }
    }
}