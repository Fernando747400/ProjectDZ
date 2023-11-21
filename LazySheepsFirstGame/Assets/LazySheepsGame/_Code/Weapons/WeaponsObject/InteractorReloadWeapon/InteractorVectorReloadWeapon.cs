using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using com.LazyGames;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractorVectorReloadWeapon : MonoBehaviour
{
    [SerializeField] private HandPoseArea handPoseArea;
    public event Action<Vector3> OnEnter; 
    public event Action<Vector3> OnExit;
    
    
    private void OnEnable()
    {
        handPoseArea.OnHandEnter.AddListener(OnHoveredEnter);
        handPoseArea.OnHandExit.AddListener(OnHoveredExit);
    }
    
    private void OnDisable()
    {
        handPoseArea.OnHandEnter.RemoveListener(OnHoveredEnter);
        handPoseArea.OnHandExit.RemoveListener(OnHoveredExit);
    }
    public void OnHoveredEnter(Hand hand)
    {
        // Debug.Log("OnHoveredEnter".SetColor("#5DF516"));
        OnEnter?.Invoke(hand.transform.position);
    }

    public void OnHoveredExit(Hand hand)
    {
        // Debug.Log("OnHoverdExit".SetColor("#F51686"));
        OnExit?.Invoke(hand.transform.position);
        
    }
  
    

    
}
