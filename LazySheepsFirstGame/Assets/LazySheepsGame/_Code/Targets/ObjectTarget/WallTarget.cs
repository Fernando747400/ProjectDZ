using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;

public class WallTarget : MonoBehaviour, IGeneralTarget
{
    #region Variables

    [Header("Wall Target")] 
    [SerializeField] private TargetsData targetsData;
    
    private string id;
    private int maxHealth;
    private TargetsType type;
    private float currentHp;

    #endregion

    #region public methods

    public string ID
    {
        get => id;
        set => id = value;
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public TargetsType Type
    {
        get => type;
        set => type = value;
    }

    public float CurrentHp
    {
        get => currentHp;
        set => currentHp = value;
    }

    #endregion

    #region Unity methods

    private void Start()
    {
        PrepareTarget();
    }
    

    #endregion
    
    #region private methods
    
    
    private void PrepareTarget()
    {
        ID = targetsData.ID;
        Type = targetsData.Type;
    }
    #endregion
}
  

