namespace MineCore.Blocks
{
    public interface IBlockState
    {
        string Name { get; }
        byte Data { get; }
    }
}