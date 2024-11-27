using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunBase : MonoBehaviour
{
    public bool isPlayer = false;
    public ProjectileBase projectilePrefab;
    public Transform shootPoint;
    public List<UIGunUpdater> uIGunUpdaters;

    //[Space(10)]
    [Header("Gun Stats")]
    public int maxShots = 6;
    public float timeBetweenShots = .3f;
    public float reloadTime = 2f;
    public float bulletSpeed = 50f;


    private Coroutine _currentCoroutine;
    
    [Space(10)]
    [SerializeField]
    public int _currentShots;
    [SerializeField]
    public bool _reloading; 
   
    void Awake()
    {
        if (isPlayer) GetAllUis();
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = shootPoint.position;
        projectile.transform.rotation = shootPoint.rotation;
    }

    IEnumerator AutoShoot()
    {
        if (_reloading)
            yield break;

        while(true)
        {
            if (_currentShots < maxShots)
            {
                Shoot();
                _currentShots++;
                UpdateUI();
                ReloadCheck();
                yield return new WaitForSeconds(timeBetweenShots);

            }
        }
    }

    public void ShootStart()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(AutoShoot());
    }

    public void ShootStop()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
    }

    private void ReloadCheck()
    {
        if (_currentShots >= maxShots)
        {
            ShootStop();
            ReloadStart();
        }
    }

    private void ReloadStart()
    {
        _reloading = true;
        _currentCoroutine = StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        float time = 0;

        while (time < reloadTime)
        {
            time += Time.deltaTime;
            uIGunUpdaters.ForEach(i => i.UpdateValue(time, reloadTime, false));
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        _reloading = false;
    }

    private void GetAllUis()
    {
        uIGunUpdaters = GameObject.FindObjectsByType<UIGunUpdater>(FindObjectsSortMode.None).ToList();
    }

    private void UpdateUI()
    {
        uIGunUpdaters.ForEach(i => i.UpdateValue(_currentShots, maxShots, true));
    }
}
