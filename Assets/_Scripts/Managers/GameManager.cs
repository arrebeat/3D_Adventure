using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArreTools.StateMachine;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        PREGAME,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE
    }

    public StateMachine<GameStates> stateMachine;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        stateMachine = new StateMachine<GameStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.PREGAME, new GMState_PREGAME());
        stateMachine.RegisterStates(GameStates.GAMEPLAY, new GMState_GAMEPLAY());
        stateMachine.RegisterStates(GameStates.PAUSE, new StateBase());
        stateMachine.RegisterStates(GameStates.WIN, new StateBase());
        stateMachine.RegisterStates(GameStates.LOSE, new StateBase());

        stateMachine.SwitchState(GameStates.PREGAME);
    }
}
