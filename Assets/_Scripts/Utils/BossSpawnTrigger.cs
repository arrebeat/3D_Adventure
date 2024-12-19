using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class BossSpawnTrigger : MonoBehaviour
{
    private BossBase _boss;
    public GameObject bossCamera;

    void OnValidate()
    {
    }

    void Awake()
    {
        GameObject bossObject = GameObject.Find("Boss");
        _boss = bossObject.GetComponent<BossBase>();
        bossCamera.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            ActivateBossCamera();
            if (_boss.stateMachine.CurrentState == null) _boss.SwitchState(BossStates.Init);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
            {
                bossCamera.SetActive(false);
            }       
    }

    public void ActivateBossCamera()
    {
        bossCamera.SetActive(true);
    }
}
