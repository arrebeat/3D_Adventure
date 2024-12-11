using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMechanic_Shoot : PlayerMechanicBase
{
    public List<GunBase> gunPrefabs;
    public Transform gunPosition;

    private GunBase _currentGun; 

    protected override void Init()
    {
        base.Init();

        CreateGun(0);

        playerControls.Astronaut.Shoot.started += Shoot_started;
        playerControls.Astronaut.Shoot.performed += Shoot_performed;
        playerControls.Astronaut.Shoot.canceled += Shoot_canceled;

        playerControls.Astronaut.Gun01.started += Gun01_started;
        playerControls.Astronaut.Gun02.started += Gun02_started;
    }


    private void CreateGun(int gunIndex)
    {
        _currentGun = Instantiate(gunPrefabs[gunIndex], gunPosition);
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        player.currentGun = _currentGun.GetComponent<GunBase>();
    }

    #region INPUT CALLBACKS
    private void Shoot_started(InputAction.CallbackContext context)
    {
        _currentGun.ShootStart();
    }
    private void Shoot_performed(InputAction.CallbackContext context)
    {
        
    }
    private void Shoot_canceled(InputAction.CallbackContext context)
    {
        _currentGun.ShootStop();
    }

    private void Gun01_started(InputAction.CallbackContext context)
    {
        if (_currentGun != gunPrefabs[0])
        {
            Destroy(_currentGun.gameObject);
            CreateGun(0);
        }
    }
    private void Gun02_started(InputAction.CallbackContext context)
    {
        if (_currentGun != gunPrefabs[1])
        {
            Destroy(_currentGun.gameObject);
            CreateGun(1);
        }
    }
    #endregion
}
