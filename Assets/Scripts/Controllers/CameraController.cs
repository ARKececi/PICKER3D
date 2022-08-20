using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] public Animator canAnimator;


        public void PlayerStart()
        {
            canAnimator.Play("Runner");
        }
    }
}