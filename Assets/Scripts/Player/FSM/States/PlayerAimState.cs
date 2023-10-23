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
    };

    public PlayerAimState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {
        if (!Input.GetButton("Fire2"))
        {
            player.cameraController.SetAimCamera(false);
            stateMachine.ChangeStateLogic(PlayerStateEnums.IDLE);
        }
    }

    public void OnStateEnter()
    {
        player.cameraController.SetAimCamera(true);
    }

    public void OnStateExit()
    {
    }
}
