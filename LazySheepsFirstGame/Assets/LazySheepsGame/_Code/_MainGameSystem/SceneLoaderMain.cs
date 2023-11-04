using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoaderMain : MonoBehaviour
{
    [Header("For Testing Only")]
    [SerializeField] private bool _loadOnAwake = false;


    [Header("Dependencies")]
    [Scene]
    [SerializeField] private string _mainManagersScene;
    [HorizontalLine(color: EColor.Red)]

    [Scene]
    [SerializeField] private List<string> _scenesToLoad;

    public List<string> ScenesToLoad { get { return _scenesToLoad; }  set { _scenesToLoad = value; } }

    private void Start()
    {
        if (_loadOnAwake)
            BeginLoad(true);
    }

    public void BeginLoad(bool unloadCurrentScenes)
    {
        if (unloadCurrentScenes) StartCoroutine(UnloadScenesAsync());
        StartCoroutine(LoadScenesAsync());
    }

    [Button]
    public void TestLoad()
    {
        StartCoroutine(LoadScenesAsync());
    }

    private IEnumerator UnloadScenesAsync() 
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene currentSceneToUnload = SceneManager.GetSceneAt(i);
            if (currentSceneToUnload.name == _mainManagersScene) { continue; } //Prevent unloading the current scene where this manager exits. It should be the main SceneManager. 
            AsyncOperation scenedUnloader = SceneManager.UnloadSceneAsync(currentSceneToUnload);

            while (!scenedUnloader.isDone)
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

        _scenesToLoad.Clear();
    }
    
}
