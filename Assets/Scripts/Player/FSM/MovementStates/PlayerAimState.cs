using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : IPlayerState
{
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.IDLE,
        PlayerMovementStateEnums.AIM_MOVE,

        PlayerMovementStateEnums.AIM_SHOOT,
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
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.AIM_MOVE);
        }

        if (!Input.GetButton("Fire2"))
        {
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.IDLE);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            player.shotstateMachine.ChangeState(PlayerShotStateEnums.ENTER);
        }
    }

    public void OnStateEnter()
    {
        player.animator.SetLayerWeight(player.animator.GetLayerIndex("PlayerUpper"), 1);
        player.cameraController.SetAimCamera();
    }

    public void OnStateExit()
    {
    }
}
