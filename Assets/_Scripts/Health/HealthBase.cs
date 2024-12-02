using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public int maxHp = 10;
    public bool destroyOnKill = false;
    public float timeToDestroy = 1f;
    [SerializeField] private int _currentHp;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    void Start()
    {
        Init();    
    }

    protected virtual void Init()
    {
        ResetHp();
    }
    
    protected void ResetHp()
    {
        _currentHp = maxHp;
    }
    
    public void Damage(int dmg)
    {
        _currentHp -= dmg;

        if (_currentHp <= 0) Kill();

        OnDamage?.Invoke(this);
    }

    public void Damage(int dmg, Vector3 dir)
    {
        _currentHp -= dmg;

        if (_currentHp <= 0) Kill();
    }

    protected virtual void Kill()
    {
        if (destroyOnKill) Destroy(gameObject, timeToDestroy);

        OnKill?.Invoke(this);
    }
}