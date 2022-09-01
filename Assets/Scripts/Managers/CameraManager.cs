using System;
using Cinemachine;
using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private Vector3 _vmCameraPosition;

        private StateCamera _cameraState;
        #endregion

        #region Serialized Variables

        [SerializeField] private CinemachineStateDrivenCamera _vmStateCamera;

        [SerializeField] private Animator _animator;

        #endregion

        #endregion
        
        #region Event Subscription

        private void Awake()
        {
            OnGetCameraPosition();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onCameraMovePosition += OnCameraMovePosition;
            CoreGameSignals.Instance.onGetCameraPosition += OnGetCameraPosition;
            CoreGameSignals.Instance.onEnterFinisStation += OnEnterFinisStation;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onCameraMovePosition -= OnCameraMovePosition;
            CoreGameSignals.Instance.onGetCameraPosition -= OnGetCameraPosition;
            CoreGameSignals.Instance.onEnterFinisStation -= OnEnterFinisStation;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnEnterFinisStation()
        {
            switch (_cameraState)
            {
            case StateCamera.Runner:
                _cameraState = StateCamera.Init; 
                _animator.SetTrigger(_cameraState.ToString());
                break;
            
            
            case StateCamera.Init:
                _cameraState = StateCamera.Runner; 
                _animator.SetTrigger(_cameraState.ToString());
                break;
            }
            
        }
        
        private void OnGetCameraPosition()
        {
            _vmCameraPosition = transform.localPosition;
        }

        private void OnCameraMovePosition()
        {
            transform.localPosition = _vmCameraPosition;
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