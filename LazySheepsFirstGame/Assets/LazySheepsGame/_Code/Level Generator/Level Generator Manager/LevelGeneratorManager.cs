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
    private List<GameObject> _spawnedMoudles = new List<GameObject>();

    #endregion

    #region public variables

    public static LevelGeneratorManager Instance;
    

    #endregion


    #region Unity Methods

    void Awake()
    {
        
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
            GameObject spawnedModule = null;
            Module module = GetModuleType(ModuleType.Room);
            spawnedModule = Instantiate(module.gameObject, centerLevel.transform.position, Quaternion.identity);
            spawnedModule.transform.SetParent(centerLevel.transform);
            _spawnedMoudles.Add(spawnedModule);
        }

        for (int i = 0; i < corridorsCount; i++)
        {
            GameObject spawnedModule = null;
            Module module = GetModuleType(ModuleType.Corridor);
            spawnedModule = Instantiate(module.gameObject, centerLevel.transform.position, Quaternion.identity);
            spawnedModule.transform.SetParent(centerLevel.transform);
            _spawnedMoudles.Add(spawnedModule);
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
        _spawnedMoudles.Clear();
    }
    
    #endregion

    #region private Methods

    private void RepositionModules()
    {
      foreach (var module in _spawnedMoudles)
      {
          if (module == _spawnedMoudles[0])
          {
              Debug.Log("First Module".SetColor("#CFD93C"));
              continue;
          }
          Module moduleComponent = module.GetComponent<Module>();
          moduleComponent.RepositionModule(GetPreviousModule(_spawnedMoudles));
          
      }
       
    }
    
    private Module GetPreviousModule(List<GameObject> _spawnedMoudles)
    {
        foreach (var module in _spawnedMoudles)
        {
            Debug.Log("Previous Module Connector type ".SetColor("#1F43EB") + module.GetComponent<Module>().MyData.ModuleType);
            return module.GetComponent<Module>();

        }

        return null;
    }
    
    private Module GetModuleType(ModuleType moduleType)
    {
        foreach (var module in modules)
        {
            if (module.MyData.ModuleType == moduleType)
            {
                return module;
            }
        }

        return null;
    }
    
    private void CreateInstance()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    
    #region ManagerBase
   
    public override void Init()
    {
        if (FinishedLoading) return;
        CreateInstance();
        FinishedLoading = true;
        FinishLoading?.Invoke();
        
        Debug.Log("Level Generator Initialized");
        
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


