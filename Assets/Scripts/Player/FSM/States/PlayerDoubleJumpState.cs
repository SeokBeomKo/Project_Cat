using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerDoubleJumpState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {
        if (player.rigid.velocity.y <= 0.1f)
        {
            stateMachine.ChangeState(PlayerStateEnums.FALL);
            return;
        }
    }

    public void OnStateEnter()
    {
        player.animator.SetBool("isDoubleJump", true);
        player.rigid.AddForce(Vector3.up * player.jumpPower * 0.5f, ForceMode.Impulse);
    }

    public void OnStateExit()
    {
        player.animator.SetBool("isDoubleJump", false);
    }

    public void ChangeState(IPlayerState newState)
    {

    }
}
