using UnityEngine;

namespace Controllers
{
    public class LevelLoaderCommand : MonoBehaviour
    {
        public void LoaderLevel(int _levelID, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"LevelPrefabs/level {_levelID}"), levelHolder);
        }
    }
}