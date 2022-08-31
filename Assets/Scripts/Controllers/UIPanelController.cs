using System.Collections.Generic;
using Enums;
using Keys;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> panels;
        
        [SerializeField] public List<Image> Images;

        #endregion

        #region Private Variables

        private int _poolID;

        #endregion

        #endregion

        public void OnOpenPanel(UIPanel panelParam )
        {
            panels[(int) panelParam].SetActive(true);
        }

        public void OnClosePanel(UIPanel panelParam)
        {
            panels[(int) panelParam].SetActive(false);
        }
        
        public void OnOpenPoolPanel(int pool)
        {
            Images[pool].enabled = true;
        }

        public void OnClosePoolPanel()
        {
            foreach (var image in Images)
            {
                image.enabled = false;
            }
            
        }
    }
}