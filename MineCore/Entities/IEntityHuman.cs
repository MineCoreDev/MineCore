using System;
using MineCore.Data;

namespace MineCore.Entities
{
    public interface IEntityHuman : IEntityLiving
    {
        IClientData ClientData { get; set; }

        void SendPlayerSpawnPacket(IPlayer player);
        void SendSkin(IPlayer player);
    }
}