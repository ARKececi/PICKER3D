using System;
using Controllers;
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

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += movement;
            CoreGameSignals.Instance.onStation += station;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= movement;
            CoreGameSignals.Instance.onStation -= station;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void movement(HorizontalInputParams horizontalInputParams)
        {
            playerMovementController.movementcontroller(horizontalInputParams);
        }

        private void station(StationBoolParams stationBoolParams)
        {
            playerMovementController.station(stationBoolParams);
        }
    }
}