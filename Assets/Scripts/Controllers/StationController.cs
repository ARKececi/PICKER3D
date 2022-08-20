using System;
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
                CoreGameSignals.Instance.onStation.Invoke(new StationBoolParams()
                {
                    StationBool = true
                });
            }
        }
    }
}