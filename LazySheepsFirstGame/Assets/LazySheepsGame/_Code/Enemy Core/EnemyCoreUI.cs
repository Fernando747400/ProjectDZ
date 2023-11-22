using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace com.LazyGames
{
    public class EnemyCoreUI : MonoBehaviour
    {
        [SerializeField] private Slider lifetimeSlider;
        [SerializeField] private TMP_Text deactivatorTimerTxt;
        
        
        #region unity methods

        private void Start()
        {
            deactivatorTimerTxt.text = ""; 
            EnableLifeTimeUI(false);

        }

        #endregion
        
        #region public methods

        public void SetMaxValue(float value)
        {
            lifetimeSlider.maxValue = value;
            lifetimeSlider.value = value;
        }
        public void UpdateLifeTime(float value)
        {
            lifetimeSlider.value = value;
            deactivatorTimerTxt.text = value.ToString();
        }
        
        public void EnableLifeTimeUI(bool value)
        {
            lifetimeSlider.gameObject.SetActive(value);
            deactivatorTimerTxt.gameObject.SetActive(value);
        }
        
        public void UpdateDeactivatorLifeText(int value)
        {
            deactivatorTimerTxt.text = "Deactivator Life = " + value;
        }
        
        
        #endregion
    }

}
