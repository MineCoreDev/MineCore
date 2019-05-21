namespace MineCore.Entities.MetaDatas
{
    public interface IEntityDataDictionary
    {
        IEntityDataProperty GetDataProperty(int id);
        IEntityDataProperty<T> GetDataProperty<T>(int id);

        bool SetDataProperty(IEntityDataProperty dataProperty);
        bool SetDataProperty<T>(IEntityDataProperty<T> dataProperty);
        bool SetDataProperty<T>(int id, T value);
    }
}