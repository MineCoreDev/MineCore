namespace MineCore.Data
{
    public interface IResourcePack
    {
        string PackId { get; }
        string PackVersion { get; }
        ulong PackSize { get; }
        string EncryptionKey { get; }
        string SubPackName { get; }
        string ContentIdentity { get; }
        bool PackFlag { get; }
    }
}