namespace MineCore.Data
{
    public interface IRuntimeBlockData : IName
    {
        int RuntimeId { get; }
        int Id { get; }
        int Data { get; }

        int Index { get; }
    }
}