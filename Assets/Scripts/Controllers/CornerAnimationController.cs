using System;
using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class CornerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        #endregion        

        #endregion

        public void CornerAnimation()
        {
            transform.DOMoveY(0f, 2);
        }
    }
}