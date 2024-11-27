using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArreTools.StateMachine;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss = (BossBase)objs[0];
        }
    }

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] objs) 
        { 
            Debug.Log(objs + " MAH COMO");
            base.OnStateEnter(objs);
            boss.SpawnAnimation();
        }
    }
    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objs) 
        { 
            base.OnStateEnter(objs);
            boss.MoveToRandomPoint();
        }
    }
}
