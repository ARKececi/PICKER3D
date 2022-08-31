using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class DoorAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region private Variables

        private Vector3 _door;

        #endregion

        #endregion
        
        public void DoorAnimation()
        {
            _door = new Vector3(0, 90, 90);
            transform.DORotate(_door, 1);
        }
    }
}