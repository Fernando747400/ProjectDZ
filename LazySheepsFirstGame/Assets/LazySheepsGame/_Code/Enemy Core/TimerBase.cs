using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;

namespace com.LazyGames
{
    public class TimerBase : MonoBehaviour
    {
        #region public variables
        public float CurrentTimer => _currentTimer;
        public Action OnTimerEnd;
        public Action<float> OnTimerUpdate;
        #endregion

        #region private variables
        private bool _isTimerActive;
        private float _currentTimer;

        private float _updateInterval = 1f; // Intervalo de actualización del timer.
        private float _elapsedTime;

        private bool _isCountdown;
        #endregion
        
        #region unity methods
        void Update()
        {
            if (_isTimerActive)
            {
                if(_isCountdown) _currentTimer -= 1 * Time.deltaTime;
                else _currentTimer += 1 * Time.deltaTime;
                
                if (_currentTimer <= 0.9f)
                {
                    FinishedTimer();
                }
            
                _elapsedTime += Time.deltaTime;

                // Verifica si ha pasado el intervalo de actualización y llama al evento.
                if (_elapsedTime >= _updateInterval)
                {
                    OnTimerUpdate?.Invoke(_currentTimer);
                    // Debug.Log("Update Timer = " + _currentTimer);
                    _elapsedTime = 0.0f;
                }
            }
        }
        #endregion

        #region public methods

        public void StartTimer(float timer, bool isCountDown = false, float intervalUpdate = 1f,string message = "")
        {
            _isTimerActive = true;
            _currentTimer = timer;
            _updateInterval = intervalUpdate;
            _isCountdown = isCountDown;
            Debug.Log("Timer Started = " + message + " Timer = " + _currentTimer);
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
        }

        #endregion
    }

}