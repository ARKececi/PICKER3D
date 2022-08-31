using Data.UnityObject;
using Data.ValueObject;
using Keys;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Rigidbody move;

        [SerializeField] private GameObject player;

        #endregion

        #region Private Variables

        [Header("Data")] private PlayerData _playerControllerData;

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

        private PlayerData GetInputData()
        {
            return Resources.Load<SO_Player>("Data/SO_Player").playerData;
        }

        public Rigidbody rigidbody()
        {
            return move;
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
            Debug.Log(_stationBool);
        }

        public void PlayerRotationClamp()
        {
            var currentRotation = player.transform.localEulerAngles;
            currentRotation.x = Mathf.Clamp(currentRotation.x, -35, 15);
            player.transform.eulerAngles = currentRotation;
        }

        private void FixedUpdate()
        {
            if (_isTouchingPlayer)
            {
                if (!_stationBool)
                {
                    move.velocity = new Vector3(_inputforce * _playerControllerData._movementSide, move.velocity.y,
                        _playerControllerData.MoveSpeed);
                    move.position = new Vector3(Mathf.Clamp(move.position.x, _clamp.x, _clamp.y), move.position.y,
                        move.position.z); // clamp 

                    if (player.transform.eulerAngles.x < 60) PlayerRotationClamp();
                }
                else
                {
                    move.velocity = Vector3.zero;
                }
            }
        }
    }
}