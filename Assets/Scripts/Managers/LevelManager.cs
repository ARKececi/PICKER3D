using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
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

        [SerializeField] private ClearlevelController clearLevel;

        #endregion

        #region Private Variables

        [SerializeField] private int _levelID;

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
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPoolLevelID -= OnPoolLevelID;
            CoreGameSignals.Instance.onLoaderLevel -= OnLoaderLevel;
            CoreGameSignals.Instance.onClearLevel -= OnClearLevel;
            CoreGameSignals.Instance.onWinLevelID -= WinLevelID;
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
        }

        private void WinLevelID(WinLevelParams levelID)
        {
            _levelID = levelID.WinLevel;
        }

        private void OnLoaderLevel()
        {
            levelLoader.LoaderLevel(_levelID, levelHolder.transform);
        }

        private void OnClearLevel()
        {
            
            clearLevel.ClearLevel(levelHolder.transform);
        }


    }
}