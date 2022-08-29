using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
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

        [SerializeField] private int _levelID;

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
            CoreGameSignals.Instance.onWinLevelID += WinLevelID;
            CoreGameSignals.Instance.onNextLevelLoader += OnNextLevelLoader;
            CoreGameSignals.Instance.onResetLevel += OnResetLevel;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPoolLevelID -= OnPoolLevelID;
            CoreGameSignals.Instance.onLoaderLevel -= OnLoaderLevel;
            CoreGameSignals.Instance.onClearLevel -= OnClearLevel;
            CoreGameSignals.Instance.onWinLevelID -= WinLevelID;
            CoreGameSignals.Instance.onNextLevelLoader -= OnNextLevelLoader;
            CoreGameSignals.Instance.onResetLevel += OnResetLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private int OnPoolLevelID()
        {
            return _levelID;
        }

        private void Start()
        {
            OnLoaderLevel();
            _nextLevelTransform = new Vector3(0, 0, 435);
        }
        
        private void WinLevelID(WinLevelParams levelID)
        {
            _levelID = levelID.WinLevel % Resources.Load<SO_Level>("Data/SO_Level")
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
        }

        private void OnResetLevel()
        {
            OnLoaderLevel();
            levelHolder.transform.GetChild(1).position = _startLevelTransform;
        }

        private void OnClearLevel()
        {
            clearlevel.ClearLevel(levelHolder.transform);
        }


    }
}