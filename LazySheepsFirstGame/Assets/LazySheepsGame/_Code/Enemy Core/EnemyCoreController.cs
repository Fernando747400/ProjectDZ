using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames;
using Lean.Pool;
using UnityEditor;

public class EnemyCoreController : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private EnemyCoreData enemyCoreData;
    [SerializeField] private Transform[] spawnPoints;

    #endregion

    #region private variables

    private int _currentHealth;



    #endregion

    #region unity methods



    void Start()
    {
       
    }

    void Update()
    {

    }

    #endregion

    #region private methods

    public void Initialized()
    {
        _currentHealth = enemyCoreData.MaxHealth;
        Debug.Log("Init Health = ".SetColor("#F37817") + _currentHealth);
        SpawnEnemyWave();
    }
    public void ReceiveDamage(int damage)
    {
        if(_currentHealth <= 0) return;
        _currentHealth -= damage;
        
        Debug.Log("Receive damage Current Health = ".SetColor("#F73B46") + _currentHealth);
        if (_currentHealth <= 0)
        {
            Debug.Log("Enemy Core Destroyed");
            
        }
    }

    private void SpawnEnemyWave()
    {
        var enemy = LeanPool.Spawn(enemyCoreData.EnemyPrefab);
        enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        
    }

#endregion

}
#if UNITY_EDITOR_WIN

[CustomEditor(typeof(EnemyCoreController))]
public class EnemyCoreControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnemyCoreController enemyCoreController = (EnemyCoreController) target;
        if (GUILayout.Button("Initialize Core"))
        {
            enemyCoreController.Initialized();
        }
        
        if (GUILayout.Button("Receive Damage"))
        {
            
            enemyCoreController.ReceiveDamage(100);
        }
    }
}
#endif
