using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanic_CoinMagnet : PlayerMechanicBase
{
    public void OnTriggerEnter(Collider other)
    {
        var c = other.GetComponent<Magnetic>();
        if (c != null) c.enabled = true;
    }
}
