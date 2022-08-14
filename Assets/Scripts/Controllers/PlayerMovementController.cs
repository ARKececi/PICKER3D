using System;
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

        [SerializeField] private PlayerData _playerControllerData;
        
        #endregion

        #region Private Variables

        private float _inputforce;

        private Vector2 _clamp;

        #endregion

        #endregion

        public void movementcontroller(HorizontalInputParams inputParams)
        {
            _inputforce = inputParams.XValue;
            _clamp = inputParams.ClampValues;
        }

        private void FixedUpdate()
        {
            _move.velocity = new Vector3(_inputforce * _playerControllerData._movementSide,_move.velocity.y,_playerControllerData.MoveSpeed);
            _move.position = new Vector3(Mathf.Clamp(_move.position.x, _clamp.x, _clamp.y), _move.position.y, _move.position.z); // clamp
        }
    }
}