using BinaryIO;

namespace MineCore.Entities.MetaDatas
{
    public interface IEntityDataProperty
    {
        int MetaDataType { get; }
        int Id { get; }

        byte[] ToBinary();
        void FromBinary(NetworkStream stream);
    }

    public interface IEntityDataProperty<T> : IEntityDataProperty
    {
        T Value { get; }
    }
}