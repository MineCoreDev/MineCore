namespace MineCore.Entities
{
    public interface IEntity
    {
        long EntityId { get; }
        long EntityRuntimeId { get; }
    }
}