namespace MineCore.Entities.Attributes
{
    public interface IEntityAttribute
    {
        string Name { get; }
        float MaxValue { get; }
        float MinValue { get; }

        float Value { get; set; }

        float DefaultValue { get; }
    }
}