using System;
using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class CornerAnimationController : MonoBehaviour
    {
        public void CornerAnimation()
        {
            transform.DOMoveY(0f, 2);
        }
    }
}