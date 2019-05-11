using System;

namespace MineCore.Data.Impl
{
    public class LoginData : ILoginData
    {
        public string Xuid { get; set; }
        public string DisplayName { get; set; }
        public Guid ClientUUID { get; set; }
        public string IdentityPublicKey { get; set; }
    }
}