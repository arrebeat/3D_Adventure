using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skins
{
    public class SkinBase_Super : SkinBase
    {
        public int targetDamage = 30;
        public float duration = 5;
        
        public override void Collect()
        {
            base.Collect();
            player.ChangeDamage(targetDamage, duration);
        }
    }

}
