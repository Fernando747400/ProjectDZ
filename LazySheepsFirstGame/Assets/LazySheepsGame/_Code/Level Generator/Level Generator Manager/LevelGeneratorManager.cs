using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGeneratorManager : ManagerBase
{

    #region SerializedFields
    
    [Header("Modules Data")]
    [SerializeField] private List<Module> modules;
    
    [Header("Level Settings")]
    [SerializeField] private int roomsCount;
    [SerializeField] private int corridorsCount;
    [SerializeField] GameObject centerLevel;

    #endregion

    #region private variables
    private List<GameObject> _spawnedRooms = new List<GameObject>();
    private List<GameObject> _spawnedCorridors = new List<GameObject>();

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
        GenerateLevel();
    }

    #endregion


    #region Public Methods

    public void GenerateLevel()
    {
        DeleteCurrentLevel();

        for (int i = 0; i < roomsCount; i++)
        {
            GameObject spawnedModule = Instantiate(modules[0].gameObject, centerLevel.transform.position, Quaternion.identity);
            spawnedModule.transform.SetParent(centerLevel.transform);
            _spawnedRooms.Add(spawnedModule);
        }
        
        for (int i = 0; i < corridorsCount; i++)
        {
            GameObject spawnedModule = Instantiate(modules[1].gameObject, centerLevel.transform.position, Quaternion.identity);
            spawnedModule.transform.SetParent(centerLevel.transform);
            _spawnedCorridors.Add(spawnedModule);
        }
        
        RepositionModules();
    }

    public void DeleteCurrentLevel()
    {
        GameObject[] modulesSpawned = GameObject.FindGameObjectsWithTag("Module");
        
        foreach (GameObject module in modulesSpawned)
        {
           DestroyImmediate(module);
        }
        
    }
    
    #endregion

    #region private Methods

    private void RepositionModules()
    {
      
       
    }
    
  
    
    #endregion

}

#if UNITY_EDITOR_WIN
[CustomEditor(typeof(LevelGeneratorManager))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelGeneratorManager levelGeneratorManager = target as LevelGeneratorManager;
          
        if (GUILayout.Button("Generate Random Level"))
        {
            levelGeneratorManager.GenerateLevel();
        }

        if (GUILayout.Button("Clear Level"))
        {
            levelGeneratorManager.DeleteCurrentLevel();
        }
       
    }
    
}

#endif


