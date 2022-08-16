using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
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

        }

        private void UnsubscribeEvents()
        {

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            OnLoaderLevel();
        }

        private void OnLoaderLevel()
        {
            levelLoader.LoaderLevel(_levelID, levelHolder.transform);
        }
    }
}