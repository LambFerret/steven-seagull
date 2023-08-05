using player;

namespace player
{
    public interface IDataPersistence
    {
        void LoadData(PlayerData data);
        void SaveData(PlayerData data);
    }
}