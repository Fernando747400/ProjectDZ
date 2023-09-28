using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;


public interface IGeneralTarget
{
    #region Variables

    string ID { get; set; }
    int MaxHealth { get; set; }
    TargetsType Type { get; set; }
    float CurrentHp { get; set; }
    
    #endregion

    #region private methods
    
    private void TakeDamage(float dmgValue, string id ="")
    {
        CurrentHp -= dmgValue;
        Debug.Log("TookDamage = ".SetColor("#F73B46") + id + CurrentHp);
    }


    private void CheckHealth()
    {
        if (CurrentHp <= 0)
        { 
            
            // Destroy(gameObject);
        }
    }

    #endregion

    #region public methods

    public void ReceiveRaycast(float dmgValue)
    {
        TakeDamage(dmgValue);
        CheckHealth();
    }
    
    #endregion

    
    
}
