using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Self Variables

        

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onSaveGame += OnSaveGame;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onSaveGame -= OnSaveGame;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion

        private void OnSaveGame(SaveDataParams saveDataParams)
        {
            if (saveDataParams.Level != null)
            {
                ES3.Save("Level", saveDataParams.Level);
            }
        }
    }
}