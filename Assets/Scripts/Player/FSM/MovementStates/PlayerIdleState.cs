using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.MOVE,
        PlayerMovementStateEnums.JUMP,
        PlayerMovementStateEnums.DIVEROLL,
        PlayerMovementStateEnums.BACKROLL,

        PlayerMovementStateEnums.AIM,
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.FALL,
        PlayerMovementStateEnums.SHOOT,
    };

    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerIdleState(PlayerStateMachine _stateMachine)
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

        // if (Input.GetButtonDown("Fire1"))
        // {
        //     player.shotstateMachine.ChangeState(PlayerShotStateEnums.ENTER);
        //     return;
        // }
    }

    public void OnStateEnter()
    {
        ClearAimSetting();
        
        player.playerStats.FillDoubleCount();
    }

    public void OnStateExit()
    {
    }

    public void ClearAimSetting()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 0);
        player.cameraController.SetPlayCamera();
    }
}