namespace MineCore.Data
{
    public interface IClientData
    {
        string ClientRandomId { get; set; }
        int CurrentInputMode { get; set; }
        int DefaultInputMode { get; set; }
        string DeviceModel { get; set; }
        int DeviceOS { get; set; }
        string GameVersion { get; set; }
        int GUIScale { get; set; }
        string LanguageCode { get; set; }
        string ServerAddress { get; set; }
        ISkin Skin { get; set; }
        int UIProfile { get; set; }
    }
}