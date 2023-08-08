using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace player
{
    public class DataPersistenceManager : MonoBehaviour
    {
        public static DataPersistenceManager Instance { get; private set; }
        [SerializeField] private bool initializeDataIfNull;

        [Header("File Storage Config")] public string fileName;

        private PlayerData _gameData;
        private List<IDataPersistence> _dataPersistenceObjects;
        private FileDataHandler _dataHandler;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z)) SceneManager.LoadScene(0);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }

        private void OnSceneUnloaded(Scene scene)
        {
            SaveGame();
        }

        public void NewGame()
        {
            _gameData = new PlayerData();
        }

        public void LoadGame()
        {
            _dataPersistenceObjects = FindAllDataPersistenceObjects();
            _gameData = _dataHandler.Load();

            if (_gameData == null && initializeDataIfNull)
            {
                NewGame();
            }

            if (_gameData == null)
            {
                Debug.Log("No data was found. A New Game needs to be started before data can be loaded. LOAD GAME");
                return;
            }

            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(_gameData);
            }
        }

        public void SaveGame()
        {
            if (_gameData == null)
            {
                Debug.LogWarning(
                    "No data was found. A New Game needs to be started before data can be saved. SAVE GAME");
                return;
            }

            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(_gameData);
            }

            Debug.Log("save called");
            _dataHandler.Save(_gameData);
        }

        private static List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }

        public bool HasPlayerData()
        {
            return _gameData != null;
        }
    }
}