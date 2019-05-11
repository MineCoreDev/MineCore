namespace MineCore.Data
{
    public interface ISkin
    {
        string SkinId { get; set; }
        byte[] SkinData { get; set; }
        byte[] CapeData { get; set; }
        string GeometryName { get; set; }
        string GeometryData { get; set; }
    }
}