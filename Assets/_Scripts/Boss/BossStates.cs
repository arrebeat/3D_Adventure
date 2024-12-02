using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArreTools.StateMachine;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        public BossBase boss;

        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss = (BossBase)objs[0];
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            //boss.StopAllCoroutines();
        }
    }

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] objs) 
        { 
            //Debug.Log(objs + " MAH COMO");
            base.OnStateEnter(objs);
            boss.SpawnAnimation(OnSpawnFinish);
        }

        private void OnSpawnFinish()
        {
            boss.SwitchState(BossStates.Walk);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objs) 
        { 
            base.OnStateEnter(objs);
            boss.MoveToRandomPoint(OnArrive);
        }

        private void OnArrive()
        {
            boss.SwitchState(BossStates.Attack);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] objs) 
        { 
            base.OnStateEnter(objs);
            Debug.Log("ATTACK STATE ENTER");
            boss.Attack(OnAttackFinish);
        }

        private void OnAttackFinish()
        {
            boss.SwitchState(BossStates.Walk);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }

    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
}
