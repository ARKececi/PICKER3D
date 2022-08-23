﻿using System;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Rigidbody _move;

        [Header("Data")] public PlayerData _playerControllerData;
        
        #endregion

        #region Private Variables

        private float _inputforce;

        private Vector2 _clamp;

        #endregion

        #region Private Variables

        private bool _stationBool;

        private bool _isTouchingPlayer;

        #endregion

        #endregion

        private void Start()
        {
            _playerControllerData = GetInputData();
        }

        private PlayerData GetInputData() => Resources.Load<SO_Player>("Data/SO_Player").playerData;

        public Rigidbody rigidbody()
        {
            return _move;
        }
        public void IsTouchingPlayer(IsTouching touchparams)
        {
            _isTouchingPlayer = touchparams.IsTouchingPlayer;
        }
        
        public void movementcontroller(HorizontalInputParams inputParams)
        {
            _inputforce = inputParams.XValue;
            _clamp = inputParams.ClampValues;
        }

        public void station(StationBoolParams station)
        {
            _stationBool = station.StationBool;
            
        }

        private void FixedUpdate()
        {
            if (_isTouchingPlayer)
            {
                if (!_stationBool)
                {
                    _move.velocity = new Vector3(_inputforce * _playerControllerData._movementSide,_move.velocity.y,_playerControllerData.MoveSpeed);
                    _move.position = new Vector3(Mathf.Clamp(_move.position.x, _clamp.x, _clamp.y), _move.position.y, _move.position.z); // clamp  
                }
                else
                {
                    _move.velocity = Vector3.zero;
                }
            }
            
        }
    }
}