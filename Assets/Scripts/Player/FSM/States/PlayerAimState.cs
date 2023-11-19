using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
        
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.IDLE,
        PlayerStateEnums.AIM_MOVE,

        PlayerStateEnums.AIM_SHOOT,
    };

    public PlayerAimState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            stateMachine.ChangeStateLogic(PlayerStateEnums.AIM_MOVE);
        }

        if (!Input.GetButton("Fire2"))
        {
            stateMachine.ChangeStateLogic(PlayerStateEnums.IDLE);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            stateMachine.ChangeStateLogic(PlayerStateEnums.AIM_SHOOT);
        }
    }

    public void OnStateEnter()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 1);
        player.cameraController.SetAimCamera(true);
    }

    public void OnStateExit()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 0);
        player.cameraController.SetAimCamera(false);
    }
}
