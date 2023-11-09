using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractorVectorReloadWeapon : XRBaseInteractable
{
    public event Action<Vector3> OnEnter; 
    public event Action<Vector3> OnExit;
    public void OnHoveredEnter(HoverEnterEventArgs args)
    {
        // Debug.Log("OnHoveredEnter".SetColor("#5DF516"));
        OnEnter?.Invoke(args.interactor.transform.position);
    }

    public void OnHoveredExit(HoverExitEventArgs args)
    {
        // Debug.Log("OnHoverdExit".SetColor("#F51686"));
        OnExit?.Invoke(args.interactor.transform.position);
        
    }
  
    

    
}
