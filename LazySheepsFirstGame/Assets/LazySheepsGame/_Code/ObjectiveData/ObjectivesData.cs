using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectivesData", menuName = "ObjectivesData", order = 1)]
public class ObjectivesData : ScriptableObject
{
    [SerializeField] private List<Objectives> objectives;
    public List<Objectives> Objectives => objectives;
    
    [Button]
    public void ResetObjectives()
    {
        foreach (var objective in objectives)
        {
            objective.IsCompleted = false;
        }
    }
    
    
    
}

[Serializable]
public class Objectives
{
    public string ID;
    public string Objective;
    public bool IsCompleted;
    
}