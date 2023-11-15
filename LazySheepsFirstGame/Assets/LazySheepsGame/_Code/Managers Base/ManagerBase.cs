using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ReferenceRaiser))] 
public abstract class ManagerBase : MonoBehaviour
{
    public delegate void OnFinishedLoading();
    public OnFinishedLoading FinishLoading;
    
    private bool _finishedLoading = false;
    public bool FinishedLoading
    {
        get { return _finishedLoading; }
        protected set { _finishedLoading = value; }
    }
    public abstract void Init();

    private void Awake()
    {
        Init();
    }
}
