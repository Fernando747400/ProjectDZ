using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.Dio;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReciverInputShoot : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference shootRightReference;
    [SerializeField] private InputActionReference shootLeftReference;

    [Header("SO Events")]
    [SerializeField] private IntEventChannelSO inputShootEventRight;
    [SerializeField] private IntEventChannelSO inputShootEventLeft;

    private void OnEnable()
    {
        shootRightReference.action.Enable();
        shootLeftReference.action.Enable();
    }

    private void OnDisable()
    {
        shootRightReference.action.Disable();
        shootLeftReference.action.Disable();
    }

    void Start()
    {
        shootRightReference.action.performed += ctx => inputShootEventRight.RaiseEvent(1);
        shootLeftReference.action.performed += ctx => inputShootEventLeft.RaiseEvent(2);
    }
    
}


public enum HandShoot
{
    None,
    Right,
    Left
}