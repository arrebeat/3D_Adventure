using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skins
{
    public class SkinBase_Speed : SkinBase
    {
        public float targetSpeed = 30;
        public float duration = 5;
        
        public override void Collect()
        {
            base.Collect();
            player.ChangeSpeed(targetSpeed, duration);
        }
    }

}
