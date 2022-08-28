using System;
using DG.Tweening;
using Keys;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class StationController : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                DOVirtual.DelayedCall(0.05f, () =>
                    CoreGameSignals.Instance.onStation.Invoke(new StationBoolParams()
                    {
                        StationBool = true
                    }));

            }
        }
    }
}