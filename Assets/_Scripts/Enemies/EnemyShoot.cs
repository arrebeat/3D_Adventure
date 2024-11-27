using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunbase;

        public override void Update()
        {
            base.Update();
            if (gunbase._currentShots == 0 && !gunbase._reloading) gunbase.ShootStart();
        }
    }
}
