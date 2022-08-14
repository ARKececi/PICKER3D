using System;
using UnityEngine;

namespace Controllers
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _ballCount;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                _ballCount++;
            }
        }
    }
}