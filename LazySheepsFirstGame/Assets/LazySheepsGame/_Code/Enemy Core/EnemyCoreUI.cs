using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace com.LazyGames
{
    public class EnemyCoreUI : MonoBehaviour
    {
        [SerializeField] private Slider lifetimeSlider;
        
        #region public methods

        public void SetLifeTimeSlider(float value)
        {
            value = value / 100;
            Debug.Log("SetLifeTimeSlider = ".SetColor("#0D82FE") + value);
            lifetimeSlider.value = value;
        }
        
        #endregion
    }

}
