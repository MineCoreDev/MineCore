using MineCore.Entities;

namespace MineCore.BlockEntities
{
    public interface IBlockEntity : Values.IBlockPosition3d
    {
        void SpawnTo(IPlayer player);
        void DeSpawnTo(IPlayer player);

        void SpawnToAll();
        void DeSpawnToAll();

        void OnUpdate(int tick);

        void Save();
    }
}