namespace MineCore.Data.Impl
{
    public class ClientData : IClientData
    {
        public string ClientRandomId { get; set; }
        public int CurrentInputMode { get; set; }
        public int DefaultInputMode { get; set; }
        public string DeviceModel { get; set; }
        public int DeviceOS { get; set; }
        public string GameVersion { get; set; }
        public int GUIScale { get; set; }
        public string LanguageCode { get; set; }
        public string ServerAddress { get; set; }
        public ISkin Skin { get; set; }
        public int UIProfile { get; set; }
    }
}