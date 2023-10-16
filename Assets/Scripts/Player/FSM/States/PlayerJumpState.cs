using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerJumpState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        if (player.rigid.velocity.y <= 0)
        {
            stateMachine.ChangeState(PlayerStateEnums.Fall);
        }
    }


    public void OnStateEnter()
    {
        player.animator.SetBool("isJump", true);
        player.rigid.AddForce(Vector3.up * player.jumpPower, ForceMode.Impulse);
    }

    public void OnStateExit()
    {
        player.animator.SetBool("isJump", false);
    }

    public void ChangeState(IPlayerState newState)
    {

    }
}