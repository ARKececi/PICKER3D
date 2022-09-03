using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;

        [SerializeField] private LevelLoaderCommand levelLoader;

        [SerializeField] private ClearlevelController clearlevel;

        #endregion

        #region Private Variables

        [ShowInInspector] private int _levelID;

        private int _levelCount;

        private Vector3 _nextLevelTransform;

        private Vector3 _startLevelTransform;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPoolLevelID += OnPoolLevelID;
            CoreGameSignals.Instance.onLoaderLevel += OnLoaderLevel;
            CoreGameSignals.Instance.onClearLevel += OnClearLevel;
            CoreGameSignals.Instance.onNextLevelLoader += OnNextLevelLoader;
            CoreGameSignals.Instance.onResetLevel += OnResetLevel;
            CoreGameSignals.Instance.onWinStation += OnWinStation;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPoolLevelID -= OnPoolLevelID;
            CoreGameSignals.Instance.onLoaderLevel -= OnLoaderLevel;
            CoreGameSignals.Instance.onClearLevel -= OnClearLevel;
            CoreGameSignals.Instance.onNextLevelLoader -= OnNextLevelLoader;
            CoreGameSignals.Instance.onResetLevel += OnResetLevel;
            CoreGameSignals.Instance.onWinStation += OnWinStation;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void Awake()
        {
            _levelCount = GetActiveLevel();
            Debug.Log(_levelCount);
        }
        
        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;
        }

        private int OnPoolLevelID()
        {
            return _levelID;
        }

        private void Start()
        {
            WinLevelID();
            OnLoaderLevel();
            _nextLevelTransform = new Vector3(0, 0, 435);
        }
        
        private void WinLevelID()
        {
            _levelID = _levelCount % Resources.Load<SO_Level>("Data/SO_Level")
                .Levels.Count;
        }

        private void OnLoaderLevel()
        {
            
            levelLoader.LoaderLevel(_levelID, levelHolder.transform);
            
        }

        private void OnNextLevelLoader()
        {
            OnLoaderLevel();
            levelHolder.transform.GetChild(1).position = _nextLevelTransform;
                _startLevelTransform = _nextLevelTransform;
                _nextLevelTransform += new Vector3(0, 0, 430);
                CoreGameSignals.Instance.onSaveGame?.Invoke(new SaveDataParams()
                {
                    Level = _levelCount
                });
                Debug.Log(_levelCount);
        }

        private void OnResetLevel()
        {
            OnLoaderLevel();
            levelHolder.transform.GetChild(1).position = _startLevelTransform;
            CoreGameSignals.Instance.onSaveGame?.Invoke(new SaveDataParams()
            {
                Level = _levelCount
            });
            Debug.Log(_levelCount);
        }

        private void OnClearLevel()
        {
            clearlevel.ClearLevel(levelHolder.transform);
        }

        private void OnWinStation()
        {
            _levelCount += 1;
            WinLevelID();
            DOVirtual.DelayedCall(5.1f, OnClearLevel);
            OnNextLevelLoader();
            CoreGameSignals.Instance.onPozitionAndRotationFreeze?.Invoke();

        }
    }
}