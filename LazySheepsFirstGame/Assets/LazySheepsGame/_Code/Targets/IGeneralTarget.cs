using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using Lean.Pool;
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
        if(!CheckHealth()) return;

        CurrentHp -= dmgValue;
        ReactToDamage();
        Debug.Log("TookDamage = ".SetColor("#F73B46") + id + CurrentHp);
    }
    private void ReactToDamage()
    {
        
    }


    private bool CheckHealth()
    {
        if (CurrentHp <= 0)
        {
            return false;
        }

        return true;
    }

    #endregion

    #region public methods

    public void ReceiveRaycast(float dmgValue)
    {
        switch (Type)
        {
            case TargetsType.Enemy:
                Debug.Log("Enemy".SetColor("#F95342"));
                TakeDamage(dmgValue);
                break;
            case TargetsType.Object:
                Debug.Log("Object".SetColor("#F95342"));
                ReactToDamage();
                break;
        }
        
    }
    
    #endregion

    
    
}
