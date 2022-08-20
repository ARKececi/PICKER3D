using System;
using System.Collections.Generic;
using Controllers;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController panelcontroller;

        #endregion

        #region Private Variables

        private int _levelID;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            UISignals.Instance.onFail += OnFail;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onWin += OnWin;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            UISignals.Instance.onFail -= OnFail;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onWin -= OnWin;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void OnPlay()
        {
            CoreGameSignals.Instance.onIsTouching.Invoke(new IsTouching()
            {
                IsTouchingPlayer = true
            });
            panelcontroller.OnClosePanel(UIPanel.StartButton);
        }

        private void OnFail()
        {
            panelcontroller.OnOpenPanel(UIPanel.FailButton);
        }

        private void OnWin()
        {
            panelcontroller.OnOpenPanel(UIPanel.WinButton);
        }
        
        private void OnReset()
        {
            CoreGameSignals.Instance.onClearLevel?.Invoke();
            CoreGameSignals.Instance.onLoaderLevel?.Invoke();
            CoreGameSignals.Instance.onCameraMovePosition?.Invoke();
            panelcontroller.OnClosePanel(UIPanel.FailButton);
            panelcontroller.OnOpenPanel(UIPanel.StartButton);
        }

        public void Win()
        {
            _levelID = CoreGameSignals.Instance.onPoolLevelID();
            _levelID += 1;
            CoreGameSignals.Instance.onWinLevelID(new WinLevelParams()
            {
                WinLevel = _levelID
            });
            
            panelcontroller.OnClosePanel(UIPanel.WinButton);
            
            CoreGameSignals.Instance.onReset?.Invoke();
        }
        
        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void Reset()
        {
            CoreGameSignals.Instance.onReset?.Invoke();
        }
    }
}