using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractorReloadWeapon : XRBaseInteractable
{
    public event Action<Vector3> OnHoverEnter; 
    public event Action<Vector3> OnHoverExit;
    public void OnHoveredEnter(HoverEnterEventArgs args)
    {
        Debug.Log("OnHoveredEnter".SetColor("#5DF516"));
        OnHoverEnter?.Invoke(args.interactor.transform.position);
    }

    public void OnHoverdExit()
    {
        Debug.Log("OnHoverdExit".SetColor("#F51686"));
    }
  
    

    
}
