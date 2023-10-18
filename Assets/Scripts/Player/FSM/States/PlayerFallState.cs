using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerFallState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {
        if (player.rigid.velocity.y >= 0)
        {
            stateMachine.ChangeState(PlayerStateEnums.IDLE);
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(PlayerStateEnums.DOUBLE);
            return;
        }
    }


    public void OnStateEnter()
    {
        player.animator.SetBool("isFall", true);
    }

    public void OnStateExit()
    {
        player.animator.SetBool("isFall", false);
    }

    public void ChangeState(IPlayerState newState)
    {

    }
}
