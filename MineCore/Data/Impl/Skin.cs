namespace MineCore.Data.Impl
{
    public class Skin : ISkin
    {
        public string SkinId { get; set; }
        public byte[] SkinData { get; set; }
        public byte[] CapeData { get; set; }
        public string GeometryName { get; set; }
        public string GeometryData { get; set; }
    }
}