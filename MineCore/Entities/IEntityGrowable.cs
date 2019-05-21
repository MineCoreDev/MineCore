namespace MineCore.Entities
{
    public interface IEntityGrowable : IEntityLiving
    {
        bool IsBaby { get; }
        int GrowAge { get; }

        void OnGrowProcess();
    }
}