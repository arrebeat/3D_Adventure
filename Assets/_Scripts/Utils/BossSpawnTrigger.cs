using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class BossSpawnTrigger : MonoBehaviour
{
    private BossBase _boss;

    void OnValidate()
    {
        GameObject bossObject = GameObject.Find("Boss");
        _boss = bossObject.GetComponent<BossBase>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (_boss.stateMachine.CurrentState == null)
        {
            if (other.CompareTag("Player")) _boss.SwitchState(BossStates.Init);
        }
    }
}
