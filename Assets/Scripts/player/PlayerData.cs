namespace player
{
    public class PlayerData
    {
        public bool[] ClearedLevels;
        public int[] ClearedTimes;
        public int[] ClearedStar;

        public PlayerData()
        {
            ClearedLevels = new bool[7];
            ClearedTimes = new int[7];
            ClearedStar = new int[7];

            for (int i = 0; i < ClearedLevels.Length; i++)
            {
                ClearedTimes[i] = 9999;
            }
        }
    }
}