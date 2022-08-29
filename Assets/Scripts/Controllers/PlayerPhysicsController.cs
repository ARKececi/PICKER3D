using DG.Tweening;
using Keys;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {

        #region Self Variables

        #region Private Variables

        private int _levelID;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Station"))    
            {
                DOVirtual.DelayedCall(2, other.transform.parent.GetComponent<PoolController>().OnBallController);
            }

            if (other.CompareTag("LevelStart"))
            {
                /*int myInt = 0;
                DOTween.To(() => myInt, x => myInt = x, 1, 0.6f).OnComplete(() =>
                {
                    CoreGameSignals.Instance.onStartLevelPlayer?.Invoke();
                    Debug.Log("Burdayım");  
                });*/
                //DOVirtual.DelayedCall(1, () => CoreGameSignals.Instance.onStartLevelPlayer?.Invoke());
                CoreGameSignals.Instance.onWin?.Invoke();
            }

            if (other.CompareTag(("WinStation")))
            {
                _levelID = CoreGameSignals.Instance.onPoolLevelID();
                _levelID += 1;
                CoreGameSignals.Instance.onWinLevelID(new WinLevelParams()
                {
                    WinLevel = _levelID
                });
                
                DOVirtual.DelayedCall(5.1f, () => CoreGameSignals.Instance.onClearLevel?.Invoke()); 
                CoreGameSignals.Instance.onNextLevelLoader?.Invoke();
                
                CoreGameSignals.Instance.onPozitionAndRotationFreeze?.Invoke();
                
            }
        }
    }
}