using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInputPCTest : MonoBehaviour
{
    protected float mouseX, mouseY;


    private void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected virtual void Update()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");
        
        transform.localRotation = Quaternion.Euler(-mouseY,mouseX,0f);
    }
}
