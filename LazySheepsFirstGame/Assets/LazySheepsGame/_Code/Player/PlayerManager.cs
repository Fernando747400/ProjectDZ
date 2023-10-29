using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {

        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerManager>();
                if (_instance == null)
                {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<PlayerManager>();
                    singletonObject.name = typeof(PlayerManager).ToString();
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return _instance;
        }
     
    }

    #region Serialized Fields

    [Header("Reload Position")]
    [SerializeField] private InteractorVectorReloadWeapon reloadPosition;

    [Header("Right Hand")]
    [SerializeField] private Transform rightHandAttachPoint;
    
    [Header("Left Hand")]
    [SerializeField] private Transform leftHandAttachPoint;
    
    #endregion


    #region public Variables

    public InteractorVectorReloadWeapon ReloadPosition => reloadPosition;
    public Transform RightHandAttachPoint => rightHandAttachPoint;
    public Transform LeftHandAttachPoint => leftHandAttachPoint;
    #endregion

    #region Unity Methods

    

    private void Awake()
    {
       CreateInstance();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    #endregion

    #region private Methods
    private void CreateInstance()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    #endregion
}
