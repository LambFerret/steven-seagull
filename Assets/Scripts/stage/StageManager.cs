using player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace stage
{
    public class StageManager : MonoBehaviour, IDataPersistence
    {
        public bool[] clearedLevels;
        public int[] clearedTimes;
        public int[] clearedDeaths;

        public GameObject stagePrefab;

        public void NewGame()
        {
            clearedLevels = new bool[10];
        }

        private void Start()
        {
            for (int i = 0; i < clearedLevels.Length; i++)
            {
                int level = i + 1;
                var stage = Instantiate(stagePrefab, transform);
                Image image = stage.transform.GetComponent<Image>();
                TextMeshProUGUI text = stage.transform.Find("text").GetComponent<TextMeshProUGUI>();
                Button button = stage.GetComponent<Button>();

                if (clearedLevels[i])
                {
                    image.color = Color.green;
                    text.text = string.Format(
                        $"Level {level}\n{clearedDeaths[i]} deaths\n Clear time : {clearedTimes[i]} seconds");
                }
                else
                {
                    image.color = Color.white;
                    text.text = string.Format($"Level {level}");
                }

                button.onClick.AddListener(() => { SceneManager.LoadScene(level); });
            }
        }

        public void LoadData(PlayerData data)
        {
            clearedLevels = data.ClearedLevels;
            clearedTimes = data.ClearedTimes;
            clearedDeaths = data.ClearedDeaths;
        }

        public void SaveData(PlayerData data)
        {
            data.ClearedLevels = clearedLevels;
        }
    }
}