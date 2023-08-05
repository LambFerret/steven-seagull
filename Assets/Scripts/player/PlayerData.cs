namespace player
{
    public class PlayerData
    {
        public bool[] ClearedLevels;
        public int[] ClearedTimes;
        public int[] ClearedDeaths;

        public PlayerData()
        {
            ClearedLevels = new bool[10];
            ClearedTimes = new int[10];
            ClearedDeaths = new int[10];
        }
    }
}