using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.DOUBLE,
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.LAND,
    };
    
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerFallState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {
        if (player.CheckGrounded())
        {
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.LAND);
            return;
        }

        player.rigid.MovePosition(player.rigid.position + Vector3.down * 0.001f);
        player.JumpInput();
    }


    public void OnStateEnter()
    {
        ClearAimSetting();
        
        player.animator.SetBool("isFall", true);
    }

    public void OnStateExit()
    {
        player.animator.SetBool("isFall", false);
    }

    public void ClearAimSetting()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 0);
        player.cameraController.SetPlayCamera();
    }
}
