using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.FALL,
        PlayerStateEnums.DOUBLE,
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.FALL,
        PlayerStateEnums.DOUBLE,
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerJumpState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeStateInput(PlayerStateEnums.DOUBLE);
            return;
        }
        if (player.rigid.velocity.y <= 0.1f)
        {
            stateMachine.ChangeStateInput(PlayerStateEnums.FALL);
            return;
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
}