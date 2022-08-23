using System;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {

        #region Self Variables

        #region Private Variables



        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Station"))
            {
                DOVirtual.DelayedCall(2, other.transform.parent.GetComponent<PoolController>().OnBallController);
            }

            if (other.CompareTag(("WinStation")))
            {
                CoreGameSignals.Instance.onWin?.Invoke();
                CoreGameSignals.Instance.onPozitionAndRotationFreeze?.Invoke();
            }
        }
    }
}