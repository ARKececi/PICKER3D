using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        

        #endregion

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Private Variables
        
        private float _currentVelocity; 
        private Vector2? _mousePosition; 
        private Vector3 _moveVector;

        private bool _isTouchingPlayer;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onIsTouching += onTouching;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onIsTouching -= onTouching;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void Awake()
        {
            Data = GetInputData();
        }

        private InputData GetInputData() => Resources.Load<SO_Input>("Data/SO_Input").InputData;

        private void onTouching(IsTouching isTouchparams)
        {
            _isTouchingPlayer = isTouchparams.IsTouchingPlayer;
        }
            
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                _mousePosition = Input.mousePosition;
            }
            
            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                if (_isTouchingPlayer)
                {
                    if (_mousePosition != null)
                    {
                        Vector2 mouseDeltaPos = (Vector2) Input.mousePosition - _mousePosition.Value;
                        
                        if (mouseDeltaPos.x > Data.HorizontalInputSpeed)
                            _moveVector.x = Data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        else if (mouseDeltaPos.x < -Data.HorizontalInputSpeed)
                            _moveVector.x = -Data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                        else
                            _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                                Data.ClampSpeed);

                        _mousePosition = Input.mousePosition;
                        
                        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                        {
                            XValue = _moveVector.x,
                            ClampValues = new Vector2(Data.ClampSides.x, Data.ClampSides.y)
                        });
                    }
                }

            }
        }
        
        private bool IsPointerOverUIElement() 
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
        
    }
}