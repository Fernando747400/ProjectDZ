using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace com.LazyGames
{
    public class EnemyCoreUI : MonoBehaviour
    {
        [SerializeField] private Slider lifetimeSlider;
        [SerializeField] private TMP_Text deactivatorLifeText;
        
        
        #region unity methods

        private void Start()
        {
            EnableLifeTimeUI(false);
            deactivatorLifeText.text = "Deactivator Life = " + 100;
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
        }
        
        public void EnableLifeTimeUI(bool value)
        {
            lifetimeSlider.gameObject.SetActive(value);
            deactivatorLifeText.gameObject.SetActive(value);
        }
        
        public void UpdateDeactivatorLifeText(int value)
        {
            deactivatorLifeText.text = "Deactivator Life = " + value;
        }
        
        
        #endregion
    }

}
