using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStiffenState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerStiffenState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
    }

    public void ChangeState(IPlayerState newState)
    {

    }
}