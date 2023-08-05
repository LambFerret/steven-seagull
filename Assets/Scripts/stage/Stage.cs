using System;
using player;
using TMPro;
using UnityEngine;

namespace stage
{
    public class Stage : MonoBehaviour, IDataPersistence
    {
        [Header("Settings")] public bool hasCertainSeagullPosition;
        public float spacing = 0.25F;

        [Header("Info")] public int level;

        [Header("Save Data")] public bool isCleared;
        public int clearedTime;
        public int clearedDeaths;

        [Header("Game Objects")] public TextMeshProUGUI levelText;
        public TextMeshProUGUI deathCount;
        public TextMeshProUGUI timer;
        public TextMeshProUGUI score;

        private GameObject[] _birds;
        private float _birdWidth;
        private float _birdOriginX;
        private float _time;
        private int _score;

        private void Start()
        {
            _birds = GameObject.FindGameObjectsWithTag("Seagull");
            _birdWidth = _birds[0].transform.Find("Body").GetComponent<SpriteRenderer>().bounds.size.x;
            _birdOriginX = _birds[0].transform.position.x;
            if (!hasCertainSeagullPosition) RelocateBirds();
            levelText.text = "Level : " + level;
            deathCount.text = "" + clearedDeaths;

            EventManager.Instance.OnScoreChanged += OnScoreChanged;
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnScoreChanged -= OnScoreChanged;
        }

        private void Update()
        {
            _time += Time.deltaTime;
            timer.text = _time.ToString("F1");
            score.text = "score : " + _score;

        }

        private void RelocateBirds()
        {
            int index = 0;
            foreach (var bird in _birds)
            {
                var birdPos = bird.transform.position;
                bird.gameObject.transform.position = new Vector3(_birdOriginX + (_birdWidth + spacing) * index,
                    birdPos.y, birdPos.z);
                index++;
            }
        }

        private void OnScoreChanged(int value)
        {
            _score += value;
        }

        public void LoadData(PlayerData data)
        {
            isCleared = data.ClearedLevels[level - 1];
            clearedTime = data.ClearedTimes[level - 1];
            clearedDeaths = data.ClearedDeaths[level - 1];
        }

        public void SaveData(PlayerData data)
        {
            if (isCleared)
            {
                data.ClearedLevels[level - 1] = true;
                if (clearedTime < data.ClearedTimes[level - 1])
                {
                    data.ClearedTimes[level - 1] = clearedTime;
                }

                if (clearedDeaths < data.ClearedDeaths[level - 1])
                {
                    data.ClearedDeaths[level - 1] = clearedDeaths;
                }
            }
        }
    }
}