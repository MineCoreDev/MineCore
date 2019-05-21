using System;
using System.Collections.Generic;
using System.Text;
using MineCore.Entities.MetaDatas;
using MineCore.Values;

namespace MineCore.Entities
{
    public interface IEntity : ILocation
    {
        string EntityIdentityName { get; }
        long EntityUniqueId { get; }
        long EntityRuntimeId { get; }

        float Width { get; }
        float Height { get; }

        IVector3 Motion { get; set; }

        float FallDistance { get; set; }

        short Fire { get; set; }
        short Air { get; set; }

        bool OnGround { get; }
        bool NoGravity { get; set; }
        bool Invulnerable { get; set; }
        bool CustomNameVisible { get; set; }

        int Dimension { get; }
        int PortalCooldown { get; set; }

        string CustomName { get; set; }

        IEntityDataDictionary DataProperties { get; set; }

        void SpawnTo(IPlayer player);
        void DeSpawnTo(IPlayer player);

        void SpawnToAll();
        void DeSpawnToAll();

        void OnUpdate(int ticks);
    }
}