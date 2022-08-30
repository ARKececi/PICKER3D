﻿using System;
using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using Enums;
using Keys;
using Microsoft.Unity.VisualStudio.Editor;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController panelcontroller;
        
        [SerializeField] private TextMeshProUGUI CurrentLevel;
        
        [SerializeField] private TextMeshProUGUI NextLevel;

        #endregion

        #region Private Variables

        private int _level;

        private int _poolID;

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
            UISignals.Instance.onPoolEnable += OnPoolEnable;
            CoreGameSignals.Instance.onPoolID += OnPoolID;
            
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            UISignals.Instance.onFail -= OnFail;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onWin -= OnWin;
            UISignals.Instance.onPoolEnable -= OnPoolEnable;
            CoreGameSignals.Instance.onPoolID -= OnPoolID;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            NextLevelText();
            OnPoolDisable();
        }

        private void CurrentLevelText()
        {
            CurrentLevel.text = $"{_level}";
        }

        private void NextLevelText()
        {
            NextLevel.text = $"{_level+1}";
        }
        
        private void OnPlay()
        {
            CoreGameSignals.Instance.onIsTouching.Invoke(new IsTouching()
            {
                IsTouchingPlayer = true
            });
            CoreGameSignals.Instance.onStation?.Invoke(new StationBoolParams()
            {
                StationBool = false
            });
            panelcontroller.OnClosePanel(UIPanel.StartButton);
        }

        private void OnReset()
        {
            CoreGameSignals.Instance.onClearLevel?.Invoke();
            CoreGameSignals.Instance.onResetLevel?.Invoke();
            
            CoreGameSignals.Instance.onCameraMovePosition?.Invoke();
            CoreGameSignals.Instance.onPlayerMovePosition?.Invoke();
            //CoreGameSignals.Instance.onPlayerMoveRotation?.Invoke();
            
            panelcontroller.OnClosePanel(UIPanel.FailButton);
            panelcontroller.OnOpenPanel(UIPanel.StartButton);
        }

        public void Win()
        {
            
            panelcontroller.OnClosePanel(UIPanel.WinButton);
            
            CoreGameSignals.Instance.onStartLevelPlayer?.Invoke();

            CoreGameSignals.Instance.onGetCameraPosition?.Invoke();
            CoreGameSignals.Instance.onGetPlayerPosition?.Invoke();

            CoreGameSignals.Instance.onStation.Invoke(new StationBoolParams()
            {
                StationBool = false
            });
        }

        public void OnPoolID(PoolPanelParams poolPanelParams)
        {
            _poolID = poolPanelParams.PoolID;
        }

        private void OnPoolEnable()
        {
            panelcontroller.OnOpenPoolPanel(_poolID);
        }

        private void OnPoolDisable()
        {
            panelcontroller.OnClosePoolPanel();
        }
        
        private void OnWin()
        {
            _level++;
            
            CurrentLevelText();
            NextLevelText();
            OnPoolDisable();
            
            panelcontroller.OnOpenPanel(UIPanel.WinButton);
        }
        
        private void OnFail()
        {
            panelcontroller.OnOpenPanel(UIPanel.FailButton);
        }
        
        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void Reset()
        {
            OnPoolDisable();
            CoreGameSignals.Instance.onReset?.Invoke();
        }
    }
}