using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;

public class LevelGeneratorManager : ManagerBase
{

    #region SerializedFields
    
    [Header("Modules Data")]
    [SerializeField] private List<Module> modules;
    
    [Header("SizeLevel")]
    [SerializeField] private Vector2 sizeLevel;
    
    [Header("Center Level")]
    [SerializeField] GameObject centerLevel;

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
        // GenerateLevel();
    }

    #endregion


    #region Private Methods

    private void GenerateLevel()
    {
        for (int i = 0; i < sizeLevel.x; i++)
        {
            GameObject spawnedModule = Instantiate(modules[0].gameObject, new Vector3(modules[0].SpawnPoints[0].gameObject.transform.position.x, 0, 0), Quaternion.identity);
            spawnedModule.transform.SetParent(centerLevel.transform);
        }

        // for (int i = 0; i < sizeLevel.y; i++)
        // {
        //     GameObject spawnedModule = Instantiate(modules[0].gameObject, new Vector3(i, 0, 0), Quaternion.Euler(0,90,0));
        //     spawnedModule.transform.SetParent(centerLevel.transform);
        // }
    }
    #endregion
    
    #region public Methods
    
    #endregion
}
