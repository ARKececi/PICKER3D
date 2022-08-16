using System;
using Cinemachine;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineVirtualCamera _vmCamera;

        #endregion

        #endregion

        private void OnSetCamera()
        {
            var player = FindObjectOfType<PlayerManager>().transform;
            _vmCamera.Follow = player;
            _vmCamera.LookAt = player;
        }
    }
}