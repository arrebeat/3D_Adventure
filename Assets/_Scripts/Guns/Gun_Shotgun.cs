using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun_Shotgun : GunBase
{
    public int bulletsPerShot = 5;
    public float shotAngle = 15f;

    public override void Shoot()
    {
        int mult = 0;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            if (i%2 == 0)
                mult++;
            
            var projectile = Instantiate(projectilePrefab, shootPoint);
            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (i % 2 == 0 ? shotAngle : -shotAngle) * mult;
        
            projectile.speed = bulletSpeed;
            projectile.transform.parent = null;
        }
        
    }
}
