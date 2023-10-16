using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerIdleState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            stateMachine.ChangeState(PlayerStateEnums.Run);
        }
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