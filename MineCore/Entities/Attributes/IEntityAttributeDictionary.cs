namespace MineCore.Entities.Attributes
{
    public interface IEntityAttributeDictionary
    {
        long EntityRuntimeId { get; }

        IEntityAttribute GetAttribute(string name);
        IEntityAttribute SetAttribute(string name, IEntityAttribute attribute);
        IEntityAttribute RemoveAttribute(string name);

        void CreateEntityAttributes();
        void CreateEntityLivingAttributes();
        void CreateEntityHumanAttributes();

        void SendAttributes();
    }
}