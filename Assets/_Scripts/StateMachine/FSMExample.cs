using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArreTools.StateMachine;

public class FSMExample : MonoBehaviour
{
    public enum ExampleEnum
    {
        State_01,
        State_02,
        State_03
    }

    public StateMachine<ExampleEnum> stateMachine;

    void Awake()
    {
        stateMachine = new StateMachine<ExampleEnum>();
    }

    void Start()
    {
        stateMachine.Init();
        stateMachine.RegisterStates(ExampleEnum.State_01, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.State_02, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.State_03, new StateBase());
    }

}
