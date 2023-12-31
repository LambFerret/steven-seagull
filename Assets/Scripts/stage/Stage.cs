using System.Collections.Generic;
using player;
using TMPro;
using UnityEngine;

namespace stage
{
    public class Stage : MonoBehaviour, IDataPersistence
    {
        [Header("Settings")] public bool hasCertainSeagullPosition;
        public float spacing = 0.25F;
        public string sentence;
        public List<string> translatedSentence;
        public List<int> wordIndexes;
        public int threeStarTime;
        public int twoStarTime;

        [Header("Info")] public int level;

        [Header("Save Data")] public bool isCleared;
        public int clearedTime;
        public int clearedStar;

        [Header("Game Objects")] public TextMeshProUGUI levelText;
        public TextMeshProUGUI timer;
        public TextMeshProUGUI score;
        public BallGenerator ballGenerator;
        public BallReceiver ballReceiver;
        public GameObject[] birds;
        public TextMeshProUGUI text;
        public VictoryPanel victoryPanel;

        private float _birdWidth;
        private float _birdOriginX;
        private float _time;
        private int _score;
        private List<string> _words;
        private Dictionary<string, string> _dictionary;
        private bool _isEnded;

        private void Start()
        {
            victoryPanel.SetLevel(level);
            victoryPanel.gameObject.SetActive(false);
            // birds = GameObject.FindGameObjectsWithTag("Seagull");
            _birdWidth = birds[0].transform.localScale.x;
            _birdOriginX = birds[0].transform.position.x;
            if (!hasCertainSeagullPosition) RelocateBirds();
            levelText.text = "Level : " + level;

            StartCoroutine(ballGenerator.SpawnBall(sentence));

            _words = new List<string>(sentence.Split(' '));
            MakeDictionary();

            ballReceiver.SetDictionary(_dictionary);
            ballReceiver.SetAnswer(translatedSentence);
            text.text = sentence;

            EventManager.Instance.OnClear += OnGameClear;
            EventManager.Instance.OnScoreChanged += OnScoreChanged;
        }

        private void MakeDictionary()
        {
            _dictionary = new Dictionary<string, string>();
            for (int j = 0; j < _words.Count; j++)
            {
                _dictionary.Add(_words[j], translatedSentence[wordIndexes[j]]);
            }
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnClear -= OnGameClear;
            EventManager.Instance.OnScoreChanged -= OnScoreChanged;
        }

        private void Update()
        {
            if (!_isEnded) _time += Time.deltaTime;
            timer.text = _time.ToString("F1");
            score.text = "score : " + _score;
        }

        private void RelocateBirds()
        {
            int index = 0;
            foreach (var bird in birds)
            {
                var birdText = bird.transform.Find("Head").Find("KeyCode").GetComponent<TextMeshPro>();
                var keyCode = bird.GetComponent<SeagullBehaviour>().keyCode;
                birdText.text = keyCode.ToString();
                var birdPos = bird.transform.position;
                bird.gameObject.transform.position = new Vector3(_birdOriginX + (_birdWidth + spacing) * index,
                    birdPos.y, birdPos.z);
                index++;

                Color color;
                if(index % 2 == 0) {
                    ColorUtility.TryParseHtmlString("#E28418FF", out color);
                    birdText.color = color;
                } else {
                    ColorUtility.TryParseHtmlString("#328BE0FF", out color);
                    birdText.color = color;
                }

                // if(keyCode == KeyCode.Q || keyCode == KeyCode.W || keyCode == KeyCode.E || keyCode == KeyCode.R) {
                //     birdText.color = new Color32(32, 89, 89, 100);
                // } else if (keyCode == KeyCode.U || keyCode == KeyCode.I || keyCode == KeyCode.O || keyCode == KeyCode.P) {
                //     birdText.color = new Color32(232, 54, 85, 100);
                // }
            }
        }

        private void OnScoreChanged(int value)
        {
            _score += value;
        }

        private void OnGameClear()
        {
            if (_isEnded) return;
            _isEnded = true;
            isCleared = true;
            clearedTime = (int)_time;
            if (clearedTime <= threeStarTime)
            {
                clearedStar = 3;
            }
            else if (clearedTime <= twoStarTime)
            {
                clearedStar = 2;
            }
            else
            {
                clearedStar = 1;
            }

            SFXPlayer.Instance.Play("Clear");
            victoryPanel.gameObject.SetActive(true);
            victoryPanel.Init(clearedStar, clearedTime);
        }

        public void LoadData(PlayerData data)
        {
            isCleared = data.ClearedLevels[level - 1];
            clearedTime = data.ClearedTimes[level - 1];
            clearedStar = data.ClearedStar[level - 1];
        }

        public void SaveData(PlayerData data)
        {
            Debug.Log("when stage save " + isCleared);
            if (isCleared)
            {
                data.ClearedLevels[level - 1] = true;
                if (clearedTime < data.ClearedTimes[level - 1])
                {
                    data.ClearedTimes[level - 1] = clearedTime;
                }

                if (clearedStar > data.ClearedStar[level - 1])
                {
                    data.ClearedStar[level - 1] = clearedStar;
                }
            }
        }
    }
}