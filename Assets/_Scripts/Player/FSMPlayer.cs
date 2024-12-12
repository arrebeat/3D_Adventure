using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArreTools.StateMachine;
using UnityEngine.PlayerLoop;
using UnityEditor.Analytics;

public class FSMPlayer : MonoBehaviour
{
    public PlayerController_Astronaut player;
    public enum PlayerStates
    {
        Idle,
        Move,
        Jump,
        Dead
    }

    public StateMachine<PlayerStates> stateMachine;

    void Awake()
    {
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerController_Astronaut>();

        stateMachine = new StateMachine<PlayerStates>();
    }

    void Start()
    {
        Init();    
    }

    private void Init()
    {
        stateMachine.Init();

        stateMachine.RegisterStates(PlayerStates.Idle, new PlayerState_Idle());
        stateMachine.RegisterStates(PlayerStates.Move, new PlayerState_Move());
        stateMachine.RegisterStates(PlayerStates.Jump, new PlayerState_Jump());
        stateMachine.RegisterStates(PlayerStates.Dead, new PlayerState_Dead());

        stateMachine.SwitchState(PlayerStates.Idle);
    }

    void Update()
    {
        //JumpCheck();
    }

    private void JumpCheck()
    {
        if (player.jumpPressed && player.grounded)
            stateMachine.SwitchState(PlayerStates.Jump);
    }
}
