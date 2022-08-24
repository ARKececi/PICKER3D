using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Keys;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Stage Data")] public StageData PoolData;

        #endregion

        #region Private Variables

        private int _ballCount;

        #endregion

        #region Serialized Variables

        [SerializeField] private int stageID;

        [SerializeField] private TextMeshPro text;
        
        [SerializeField] private DOTweenAnimation animationList;

        #endregion

        #endregion

        private void Awake()
        {
            PoolData = GetPoolData();
        }

        private StageData GetPoolData() => Resources.Load<SO_Level>("Data/SO_Level").Levels[CoreGameSignals.Instance.onPoolLevelID() % Resources.Load<SO_Level>("Data/SO_Level")
            .Levels.Count].StageList[stageID];

        private void CountBall()
        {
            text.text = $"{_ballCount}/{PoolData.RequiredBallCount}";
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                _ballCount++;
                CountBall();
            }
        }

        public void OnBallController()
        {
            if (_ballCount >= PoolData.RequiredBallCount)
            {
                animationList.DOPlay();
                
                DOVirtual.DelayedCall(2, () =>
                    CoreGameSignals.Instance.onStation?.Invoke(new StationBoolParams()
                    {
                        StationBool = false
                    }));
                Debug.Log("başarılı");
                
                Debug.Log(_ballCount);
                
            }

            else
            {
                UISignals.Instance.onFail?.Invoke();
            }
            OnBallCountZero();
            
            Debug.Log(_ballCount);
        }

        private void OnBallCountZero()
        {
            _ballCount = 0;
        }
        
    }
}