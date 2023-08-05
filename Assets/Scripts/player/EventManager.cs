using System;
using UnityEngine;

namespace player
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one Game Events Manager in the scene.");
            }

            Instance = this;
        }

        public event Action<int> OnScoreChanged;

        public void ScoreChanged(int value)
        {
            OnScoreChanged?.Invoke(value);
        }

        public event Action OnClear;

        public void Clear()
        {
            OnClear?.Invoke();
        }
    }
}