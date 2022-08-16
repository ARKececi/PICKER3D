using System;
using Keys;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class StationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        #endregion

        #region Private Variables

       

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CoreGameSignals.Instance.onStation.Invoke(new StationBoolParams()
                {
                    StationBool = true
                });
            }
        }
    }
}