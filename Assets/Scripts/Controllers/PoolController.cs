using System;
using Data.ValueObject;
using UnityEngine;

namespace Controllers
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Stage Data")] public StageData PoolData;

        #endregion

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