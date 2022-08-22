using UnityEngine;

namespace Controllers
{
    public class PlayerLoaderCommand : MonoBehaviour
    {
        public void LoaderPlayer( Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"PlayerPrefabs/Player"), levelHolder);
        }
    }
}