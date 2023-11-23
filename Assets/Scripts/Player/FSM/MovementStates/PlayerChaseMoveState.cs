using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaseMoveState : IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.CHASE_IDLE,
        PlayerMovementStateEnums.CHASE_FALL,
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerChaseMoveState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        if (!player.CheckGrounded())
        {
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.CHASE_FALL);
            return;
        }

        // player.animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        player.animator.SetFloat("Vertical", 1);

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.CHASE_IDLE);
            return;
        }

        player.ChaseMoveInput();
    }

    public void OnStateEnter()
    {
        player.addMoveSpeed = 10f;
        player.animator.SetBool("isMove",true);
    }

    public void OnStateExit()
    {
        player.animator.SetFloat("Horizontal", 0);
        player.animator.SetFloat("Vertical", 0);
        player.animator.SetBool("isMove",false);

        player.moveDirection = Vector3.zero;
        player.rigid.velocity = Vector3.zero;
    }
}