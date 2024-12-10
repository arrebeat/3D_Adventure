using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ActionHealthPack : MonoBehaviour
{
    public ItemManager itemManager { get; private set;}
    public PlayerController_Astronaut player { get; private set;}
    public SOInt soInt { get; private set;}

    void OnValidate()
    {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController_Astronaut>();
    }

    void Start()
    {
        soInt = itemManager.GetItemByType(ItemType.HEALTHPACK).soInt;
    }

    public void ConsumeHealthPack()
    {
        if (soInt.value > 0)
        {
            itemManager.RemoveItemByType(ItemType.HEALTHPACK);
            player.healthBase.ResetHp();
        }
    }
}
