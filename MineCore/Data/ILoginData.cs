using System;

namespace MineCore.Data
{
    public interface ILoginData
    {
        string Xuid { get; set; }
        string DisplayName { get; set; }
        Guid ClientUUID { get; set; }
        string IdentityPublicKey { get; set; }
    }
}