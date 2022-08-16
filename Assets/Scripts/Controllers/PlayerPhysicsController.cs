using System;
using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Station"))
            {
                DOVirtual.DelayedCall(2, other.transform.parent.GetComponent<PoolController>().OnBallController);
            }
        }
    }
}