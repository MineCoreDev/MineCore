namespace MineCore.Data.Impl
{
    public class ResourcePackInfo : IResourcePack
    {
        public string PackId { get; }
        public string PackVersion { get; }
        public ulong PackSize { get; }
        public string EncryptionKey { get; }
        public string SubPackName { get; }
        public string ContentIdentity { get; }
        public bool PackFlag { get; }
    }
}