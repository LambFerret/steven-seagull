using System;
using System.Collections;
using player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

namespace stage
{
    public class VictoryPanel : MonoBehaviour, IDataPersistence
    {
        private int _currentMap;
        private int _currentTime;
        private int _previousTime;
        private int _currentScore;

        [Header("Game Objects")] public Text previousTimeText;
        public Text currentTimeText;
        public GameObject leftStar;
        public GameObject middleStar;
        public GameObject rightStar;


        public void SetLevel(int level)
        {
            _currentMap = level;
        }

        public void Init(int score, int time)
        {
            _currentTime = time;
            _currentScore = score;
            if (score >= 1) StartCoroutine(ShakeStar(middleStar));
            if (score >= 2) StartCoroutine(ShakeStar(leftStar));
            if (score >= 3) StartCoroutine(ShakeStar(rightStar));
        }

        private static IEnumerator ShakeStar(GameObject star)
        {
            star.SetActive(true);
            star.transform.DOShakeScale(0.5F, 0.5F, 10, 90, false);
            yield return new WaitForSeconds(0.5F);
        }

        private void Update()
        {
            previousTimeText.text = "Previous Time : " + _previousTime;
            currentTimeText.text = "Current Time : " + _currentTime;
        }


        public void NextMap()
        {
            SceneManager.LoadScene((_currentMap + 1).ToString());
        }

        public void RestartMap()
        {
            SceneManager.LoadScene(_currentMap.ToString());
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("StageSelect");
        }

        public void LoadData(PlayerData data)
        {
            _previousTime = data.ClearedTimes[_currentMap];
        }

        public void SaveData(PlayerData data)
        {
        }
    }
}