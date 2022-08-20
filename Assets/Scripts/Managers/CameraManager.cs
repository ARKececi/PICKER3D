using System;
using Cinemachine;
using Controllers;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private Vector3 _vmCameraPosition;

        #endregion

        #region Serialized Variables

        [SerializeField] private CinemachineVirtualCamera _vmcamera;

        [SerializeField] private CinemachineStateDrivenCamera _vmStateCamera;

        #endregion

        #endregion
        
        #region Event Subscription

        private void Awake()
        {
            GetCameraPosition();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onCameraMovePosition += OnCameraMovePosition;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onCameraMovePosition -= OnCameraMovePosition;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void GetCameraPosition()
        {
            _vmCameraPosition = _vmcamera.transform.localPosition;
        }

        private void OnCameraMovePosition()
        {
            _vmcamera.transform.localPosition = _vmCameraPosition;
        }

        private void OnPlay()
        {
            OnSetCamera();
            //CameraController.PlayerStart();
        }

        private void OnSetCamera()
        {
            var player = FindObjectOfType<PlayerManager>().transform;
            _vmStateCamera.Follow = player;
        }
    }
}