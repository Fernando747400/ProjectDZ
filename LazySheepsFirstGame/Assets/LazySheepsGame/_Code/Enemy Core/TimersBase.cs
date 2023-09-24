using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;

namespace com.LazyGames
{
    public class TimersBase : MonoBehaviour
    {

        public TimersBase(float seconds)
        {
            _currentTimer = seconds;
        }
        
        public Action OnTimerEnd;
        private bool _isTimerActive;
        private float _currentTimer;

        void Update()
        {
            if (_isTimerActive)
            {
                _currentTimer -= 1f * Time.deltaTime;
                // Debug.Log("Timer = ".SetColor("#0D82FE") + _currentTimer);
                if (_currentTimer <= 0.9f)
                {
                    FinishedTimer();
                }
            }
        }

        #region public methods

        public void StartTimer(float seconds, string message = "")
        {
            _isTimerActive = true;
            _currentTimer = seconds;
            Debug.Log("Timer Started = ".SetColor("#0DFEA3") + message);
        }

        public void PauseTimer()
        {
            _isTimerActive = false;
        }

        public void ResetTimer()
        {
            _currentTimer = 0;
        }

        #endregion

        #region private methods

        private void FinishedTimer()
        {
            _isTimerActive = false;
            OnTimerEnd?.Invoke();
            Debug.Log("Timer Finished".SetColor("#0DFEA3"));
        }

        #endregion
    }

}