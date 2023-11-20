using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.MOVE,
        PlayerStateEnums.JUMP,
        PlayerStateEnums.DIVEROLL,
        PlayerStateEnums.BACKROLL,

        PlayerStateEnums.AIM,
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.FALL,
        PlayerStateEnums.SHOOT,
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
            stateMachine.ChangeStateLogic(PlayerStateEnums.FALL);
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            stateMachine.ChangeStateLogic(PlayerStateEnums.SHOOT);
            return;
        }
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
        player.cameraController.SetAimCamera(false);
    }
}