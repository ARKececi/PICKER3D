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

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
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
        
        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }
    }
}