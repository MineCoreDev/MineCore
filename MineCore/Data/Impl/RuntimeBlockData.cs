namespace MineCore.Data.Impl
{
    public class RuntimeBlockData : IRuntimeBlockData
    {
        public string Name { get; }
        public int RuntimeId { get; }
        public int Id { get; }
        public int Data { get; }

        public int Index { get; }

        public RuntimeBlockData(string name, int runtimeId, int id, int data, int index)
        {
            Name = name;
            RuntimeId = runtimeId;
            Id = id;
            Data = data;

            Index = index;
        }
    }
}