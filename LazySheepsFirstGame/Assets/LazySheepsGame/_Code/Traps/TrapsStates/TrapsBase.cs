using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames;
public class TrapsBase : StateManager<TrapsStates,TrapsBase>
{
    [SerializeField] private TrapsStates initialState;
    private void Awake()
    {
        PrepareSates();
    }

    private void PrepareSates()
    {
        States.Add(TrapsStates.Inactive, new TrapInnactiveState(TrapsStates.Inactive, this));
        States.Add(TrapsStates.Active, new TrapActiveState(TrapsStates.Active, this));
        States.Add(TrapsStates.Triggered, new TrapTriggeredState(TrapsStates.Triggered, this));
        States.Add(TrapsStates.Disabled, new TrapDisabledState(TrapsStates.Disabled, this));
        States.Add(TrapsStates.Destroyed, new TrapDestroyedState(TrapsStates.Destroyed, this));
        
        CurrentState = States[initialState];
    }
    
    public virtual void ActivateTrap()
    {
        TransitionToState(TrapsStates.Active);
        Debug.Log("Trap Activated! = ".SetColor("#FED744") + gameObject.name);
    }
    
    public virtual void DeactivateTrap()
    {
        TransitionToState(TrapsStates.Inactive);
        Debug.Log("Trap Deactivated! = ".SetColor("#FED744") + gameObject.name);
    }
    
    public virtual void ResetTrap()
    {
        TransitionToState(TrapsStates.Inactive);
        Debug.Log("Trap Reset! = ".SetColor("#FED744") + gameObject.name);
    }
    
    public virtual void TriggerTrap()
    {
        TransitionToState(TrapsStates.Triggered);
        Debug.Log("Trap Triggered! = ".SetColor("#FED744") + gameObject.name);
    }
    
    public virtual void DestroyTrap()
    {
        TransitionToState(TrapsStates.Destroyed);
        Debug.Log("Trap Destroyed! = ".SetColor("#FED744") + gameObject.name);
    }
    
    public virtual void DisableTrap()
    {
        TransitionToState(TrapsStates.Disabled);
        Debug.Log("Trap Disabled! = ".SetColor("#FED744") + gameObject.name);
    }
}



