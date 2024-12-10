using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructibleBase : MonoBehaviour
{
    public HealthBase healthBase { get; private set; }
    public Transform model;

    [Header("Hit")]
    public float shakeDuration = .1f;
    public int vibrato = 5;
    public float randomness = 90;

    [Header("Drop")]
    public GameObject coinPrefab;
    public Transform spawnPosition;
    public Vector3 launchDirection;
    public float launchDirectionRangeX = 2f;
    public float launchDirectionRangeZ = 2f;


    void OnValidate()
    {
        healthBase = GetComponent<HealthBase>();
    }

    void Awake()
    {
        healthBase.OnDamage += Damage;
    }

    [NaughtyAttributes.Button]
    private void DamageButton()
    {
        Damage(healthBase);
    }
    private void Damage(HealthBase h)
    {
        model.DOShakePosition(shakeDuration, Vector3.up, vibrato, randomness);
        model.DOShakeScale(shakeDuration, 1, vibrato);
        
        if (healthBase.CurrentHp() > 0) 
        {
            DropCoin();
        }
    }

    private void DropCoin()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = spawnPosition.position;
        i.transform.DOScale(0, shakeDuration).From();

        launchDirection.x = UnityEngine.Random.Range(launchDirectionRangeX, -launchDirectionRangeX);
        launchDirection.z = UnityEngine.Random.Range(launchDirectionRangeZ, -launchDirectionRangeZ);
        i.GetComponent<Rigidbody>().AddForce(launchDirection, ForceMode.Impulse);
    }
}
