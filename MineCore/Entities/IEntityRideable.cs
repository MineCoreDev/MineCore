namespace MineCore.Entities
{
    public interface IEntityRideable : IEntity
    {
        void OnMount(IEntity entity, int seatId);
        void OnDismount(int seatId);

        bool IsMountedEntity(int seatId);
        IEntity GetMountedEntity(int seatId);
    }
}