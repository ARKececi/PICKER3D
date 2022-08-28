using System;
using Controllers;
using DG.Tweening;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;
        
        [SerializeField] private GameObject PlayerHolder;
        
        [SerializeField] private PlayerLoaderCommand PlayerLoader;

        [SerializeField] private DOTweenAnimation Animation;

        #endregion

        #region Private Variables

        private Vector3 _playerPosition;
        private Quaternion _playerRotation;
        private Rigidbody _player;

        #endregion

        #region Public Variables

        

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnMovement;
            CoreGameSignals.Instance.onStation += OnStation;
            CoreGameSignals.Instance.onIsTouching += onTouching;
            CoreGameSignals.Instance.onLoaderPlayer += OnLoaderPlayer;
            CoreGameSignals.Instance.onPlayerMovePosition += OnPlayerMovePosition;
            CoreGameSignals.Instance.onGetPlayerPosition += OnGetPlayerPosition;
            CoreGameSignals.Instance.onPozitionAndRotationFreeze += OnPozitionAndRotationFreeze;
            CoreGameSignals.Instance.onPlayerMoveRotation += OnPlayerMoveRotation;
            CoreGameSignals.Instance.onStartLevelPlayer += OnStartLevelPlayer;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnMovement;
            CoreGameSignals.Instance.onStation -= OnStation;
            CoreGameSignals.Instance.onIsTouching -= onTouching;
            CoreGameSignals.Instance.onLoaderPlayer -= OnLoaderPlayer;
            CoreGameSignals.Instance.onPlayerMovePosition -= OnPlayerMovePosition;
            CoreGameSignals.Instance.onGetPlayerPosition += OnGetPlayerPosition;
            CoreGameSignals.Instance.onPozitionAndRotationFreeze -= OnPozitionAndRotationFreeze;
            CoreGameSignals.Instance.onPlayerMoveRotation -= OnPlayerMoveRotation;
            CoreGameSignals.Instance.onStartLevelPlayer -= OnStartLevelPlayer;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            OnLoaderPlayer();
            OnGetPlayerPosition();
            OnGetPlayerRotation();
        }

        private void OnPozitionAndRotationFreeze()
        {
            
            _player = playerMovementController.rigidbody();
            _player.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            
            
            _player.useGravity = true;
        }
        
        private void OnGetPlayerRotation()
        {
            _playerRotation = PlayerHolder.transform.localRotation;
        }

        public void OnPlayerMoveRotation()
        {
            PlayerHolder.transform.localRotation = _playerRotation;
            _player.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY |RigidbodyConstraints.FreezeRotationX;
            _player.useGravity = false;
        }

        public void OnStartLevelPlayer()
        {
            PlayerHolder.transform.localPosition = new Vector3(0.0f, 0.0f, PlayerHolder.transform.position.z);
            OnPlayerMoveRotation();
        }
        
        private void OnGetPlayerPosition()
        {
            _playerPosition = PlayerHolder.transform.localPosition;
        }

        public void OnPlayerMovePosition()
        {
            PlayerHolder.transform.localPosition = _playerPosition;
        }

        private void OnLoaderPlayer()
        {
            PlayerLoader.LoaderPlayer(PlayerHolder.transform);
        }
        
        private void onTouching(IsTouching isTouchparams)
        {
            playerMovementController.IsTouchingPlayer(isTouchparams);
        }

        private void OnMovement(HorizontalInputParams horizontalInputParams)
        {
            playerMovementController.movementcontroller(horizontalInputParams);
        }

        private void OnStation(StationBoolParams stationBoolParams)
        {
            playerMovementController.station(stationBoolParams);
        }
        
        
    }
}