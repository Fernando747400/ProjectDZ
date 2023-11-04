using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using com.LazyGames.Dio;

public class SceneLoaderMain : MonoBehaviour
{
    [Header("For Testing Only")]
    [SerializeField] private bool _loadOnAwake = false;

    [Button]
    public void TestLoad()
    {
        StartCoroutine(LoadScenesAsync());
    }

    [Header("Dependencies")]

    [Required]
    [Tooltip("Adds new scenes trought String event. Starts loading using the Bool event")]
    [SerializeField] private GenericDataEventChannelSO _sceneLoaderChannel;

    [Header("Always loaded scenes")]
    [Scene]
    [SerializeField] private List<string> _alwaysLoadedScenes;
    [HorizontalLine(color: EColor.Red)]

    [Header("Scenes to load")]
    [Scene]
    [Tooltip("Always clears this list after loading them")]
    [SerializeField] private List<string> _scenesToLoad;


    public List<string> ScenesToLoad { get { return _scenesToLoad; }  set { _scenesToLoad = value; } }

    private void OnEnable()
    {
        _sceneLoaderChannel.StringEvent += AddSceneToLoad;
        _sceneLoaderChannel.BoolEvent += BeginLoad;
    }

    private void OnDisable()
    {
        _sceneLoaderChannel.StringEvent -= AddSceneToLoad;
        _sceneLoaderChannel.BoolEvent -= BeginLoad;
    }

    private void Start()
    {
        StartCoroutine(LoadAlwaysLoadedScenes());
        if (_loadOnAwake) BeginLoad(true);
    }

    public void BeginLoad(bool unloadCurrentScenes)
    {
        if (unloadCurrentScenes) StartCoroutine(UnloadScenesAsync());
        StartCoroutine(LoadScenesAsync());
    }
 
    private IEnumerator UnloadScenesAsync() 
    {
        for (int i = SceneManager.sceneCount - 1; i >= 0; i--)
        {
            Scene currentSceneToUnload = SceneManager.GetSceneAt(i);
            if (_alwaysLoadedScenes.Contains(currentSceneToUnload.name)) { continue; } // Prevent unloading the current scene where this manager exists; it should be the main SceneManager.

            AsyncOperation sceneUnloader = SceneManager.UnloadSceneAsync(currentSceneToUnload);

            while (!sceneUnloader.isDone)
            {
                yield return null;
            }

            Debug.Log("Finished unloading " + currentSceneToUnload.name + " scene async");
        }
    }

    private IEnumerator LoadScenesAsync()
    {
        foreach (string sceneToLoad in _scenesToLoad)
        {
           AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

            while (!sceneLoader.isDone)
            {
                yield return null;
            }

            Debug.Log("Finished loading " + sceneToLoad + " scene async");
        }

        SetActiveScene();
        _scenesToLoad.Clear();
    }

    private void AddSceneToLoad(string scene)
    {
        _scenesToLoad.Add(scene);
    }

    private IEnumerator LoadAlwaysLoadedScenes()
    {
        foreach(string sceneToLoad in _alwaysLoadedScenes)
        {
            AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

            while (!sceneLoader.isDone)
            {
                yield return null;
            }
        }
    }
    
    private void SetActiveScene()
    {
        if (_scenesToLoad.Contains("VerticalSlice_0.1"))
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("VerticalSlice_0.1"));
        }
        else if (_scenesToLoad.Contains("TabernMenu"))
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("TabernMenu"));
        }
    }
}
