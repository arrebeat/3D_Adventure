using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerMechanicBase : MonoBehaviour
{
    protected PlayerController_Astronaut player;

    protected PlayerControls playerControls;


    void Awake()
    {
        if (player == null) player = GetComponent<PlayerController_Astronaut>();
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Enable();
        
        Init();
        RegisterListeners();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnDestroy()
    {
        playerControls.Disable();
        
        RemoveListeners();
    }


    protected virtual void Init() { }
    protected virtual void RegisterListeners() { }
    protected virtual void RemoveListeners() { }
}
