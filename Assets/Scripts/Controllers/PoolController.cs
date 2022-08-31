using System;
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

        [SerializeField] public int stageID;
        [SerializeField] private TextMeshPro text;
        [SerializeField] private List<DOTweenAnimation> animationList;
        [SerializeField] private GameObject ball;

        #endregion

        #endregion

        private void Awake()
        {
            PoolData = GetPoolData();
        }

        private void Start()
        {
            SetRequiredBallCount();
        }

        private StageData GetPoolData() => Resources.Load<SO_Level>("Data/SO_Level")
            .Levels[CoreGameSignals.Instance.onPoolLevelID() % Resources.Load<SO_Level>("Data/SO_Level").Levels.Count]
            .StageList[stageID];

        private void SetRequiredBallCount()
        {
            text.text = $"0/{PoolData.RequiredBallCount}";
        }

        private void CountBall()
        {
            text.text = $"{_ballCount}/{PoolData.RequiredBallCount}";
        }

        private void BallSetActive()
        {
            ball.SetActive(false);
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
                UISignals.Instance.onPoolEnable?.Invoke();
                foreach (var animation in animationList)
                {
                    animation.DOPlay();
                }

                BallSetActive();
                DOVirtual.DelayedCall(2,
                    () => CoreGameSignals.Instance.onStation?.Invoke(new StationBoolParams() { StationBool = false }));
            }
            else
            {
                UISignals.Instance.onFail?.Invoke();
            }

            OnBallCountZero();
        }

        private void OnBallCountZero()
        {
            _ballCount = 0;
        }
    }
}