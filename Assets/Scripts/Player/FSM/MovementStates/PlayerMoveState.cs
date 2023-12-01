using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.JUMP,
        PlayerMovementStateEnums.DIVEROLL,

        PlayerMovementStateEnums.AIM_MOVE
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.IDLE,
        PlayerMovementStateEnums.FALL,
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerMoveState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        if (!player.CheckGrounded())
        {
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.FALL);
            return;
        }

        player.animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        player.animator.SetFloat("Vertical", Input.GetAxis("Vertical"));

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.IDLE);
            return;
        }

        player.MoveInput();
    }

    public void OnStateEnter()
    {
        ClearAimSetting();
        
        player.animator.SetBool("isMove",true);
        player.playerStats.FillDoubleCount();
    }

    public void OnStateExit()
    {
        player.animator.SetFloat("Horizontal", 0);
        player.animator.SetFloat("Vertical", 0);
        player.animator.SetBool("isMove",false);

        player.moveDirection = Vector3.zero;
        player.rigid.velocity = Vector3.zero;
    }

    public void ClearAimSetting()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 0);
        player.cameraController.SetPlayCamera();
    }
}