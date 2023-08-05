namespace player
{
    public class PlayerData
    {
        public bool[] ClearedLevels;
        public int[] ClearedTimes;
        public int[] ClearedStar;

        public PlayerData()
        {
            ClearedLevels = new bool[10];
            ClearedTimes = new int[10];
            ClearedStar = new int[10];

            for (int i = 0; i < ClearedLevels.Length; i++)
            {
                ClearedTimes[i] = 9999;
            }
        }
    }
}