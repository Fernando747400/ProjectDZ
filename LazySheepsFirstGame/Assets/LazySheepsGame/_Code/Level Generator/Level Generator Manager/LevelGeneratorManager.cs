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
    [SerializeField] private int sizeLevel;
    [SerializeField] GameObject centerLevel;
    [SerializeField] private int selectedModule;

    #endregion

    #region private variables
    private List<GameObject> _modulesSpawned = new List<GameObject>();

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


    #region Private Methods

    public void GenerateLevel()
    {
        DeleteCurrentLevel();
        for (int i = 0; i < sizeLevel; i++)
        {
            GameObject spawnedModule = Instantiate(modules[selectedModule].gameObject, new Vector3(0,0,0), Quaternion.identity);
            Module module = spawnedModule.GetComponent<Module>();
            module.Initialize();
            spawnedModule.transform.SetParent(centerLevel.transform);
            _modulesSpawned.Add(spawnedModule);
            RepositionModule(module);
            
            
        }
        
    }

    public void DeleteCurrentLevel()
    {
        GameObject[] modulesSpawned = GameObject.FindGameObjectsWithTag("Module");
        
        foreach (GameObject module in modulesSpawned)
        {
           DestroyImmediate(module);
        }
        
    }
    
    private void RepositionModule(Module module)
    {
      
        if(_modulesSpawned.Count == 0)
        {
            module.transform.position = centerLevel.transform.position;
            return;
        }
        
        Vector3 positionPivot = module.GetPositionPivot();
        Module previousModule = _modulesSpawned[_modulesSpawned.Count - 1].GetComponent<Module>();
        Vector3 previousConnectionPoint = previousModule.GetRandomPosition();
        module.transform.position = previousConnectionPoint - positionPivot;
        
    }
    
   
    
    #endregion
    
    #region public Methods
    
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


