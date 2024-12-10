using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class ItemBase_Coin : ItemBase
{
    
    //public int meshRenderer;
    //public ParticleSystem particleSystem_Aura;

    void Awake()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
        //feedbacks = GetComponentInChildren<MMF_Player>();
        //particleSystem_Aura = GetComponentInChildren<ParticleSystem>();
        //player = GetComponent<PlayerController_Astronaut>();
    }

    void Start()
    {
        //meshRenderer.enabled = false;
        //particleSystem_Aura.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(tagPlayer))
        {
            Collected();
        }
    }

    protected override void Collected()
    {
        base.Collected();
        //itemManager.AddItemByType(ItemType.COIN);
    }
}
