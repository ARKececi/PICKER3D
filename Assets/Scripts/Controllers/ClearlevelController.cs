using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class ClearlevelController : MonoBehaviour
    {
        public void ClearLevel(Transform levelHolder)
        {
            
            Destroy(levelHolder.GetChild(0).gameObject);
        }
    }
}