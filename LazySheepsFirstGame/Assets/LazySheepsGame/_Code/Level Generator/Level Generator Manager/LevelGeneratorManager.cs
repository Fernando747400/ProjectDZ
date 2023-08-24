using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;

public class LevelGeneratorManager : ManagerBase
{

    #region SerializedFields

    

    #endregion

    #region private variables

    #endregion

    #region public variables

    public static LevelGeneratorManager Instance;
    

    #endregion


    #region Unity Methods

    void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("LevelGeneratorManager Started".SetColor("#CFD93C"));
    }

    #endregion


    #region Private Methods

    #endregion
    
    #region public Methods
    
    #endregion
}
