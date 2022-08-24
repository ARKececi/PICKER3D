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

        #endregion

        #region Private Variables

        private Vector3 _playerPosition;
        private Quaternion _playerRotation;
        private Rigidbody _player;

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

        private void OnPlayerMoveRotation()
        {
            PlayerHolder.transform.localRotation = _playerRotation;
            _player.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY |RigidbodyConstraints.FreezeRotationX;
            _player.useGravity = false;
        }

        
        private void OnGetPlayerPosition()
        {
            _playerPosition = PlayerHolder.transform.localPosition;
        }

        private void OnPlayerMovePosition()
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