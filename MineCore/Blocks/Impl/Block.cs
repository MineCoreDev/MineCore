namespace MineCore.Blocks.Impl
{
    public class Block : IBlock
    {
        public string Name { get; }
        public int BlockId { get; }
        public IBlockState[] BlockStateList { get; }
        public IBlockState BlockState { get; }

        public Block() : this(0)
        {
        }

        public Block(int data)
        {
            BlockState = BlockStateList[data];
        }
    }
}