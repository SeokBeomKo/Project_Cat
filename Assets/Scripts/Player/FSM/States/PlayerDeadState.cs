using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerDeadState(PlayerStateMachine _stateMachine)
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
