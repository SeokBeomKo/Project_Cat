using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.FALL,
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {

    };
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
            stateMachine.ChangeStateLogic(PlayerStateEnums.FALL);
            return;
        }
    }

    public void OnStateEnter()
    {
        player.animator.SetBool("isDoubleJump", true);
        player.Jump();
    }

    public void OnStateExit()
    {
        player.animator.SetBool("isDoubleJump", false);
    }
}
