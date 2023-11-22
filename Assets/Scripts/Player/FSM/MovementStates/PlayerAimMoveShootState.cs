using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimMoveShootState : IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.AIM_MOVE,
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerAimMoveShootState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {
        player.MoveInput();

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.AIM);
            return;
        }

        stateMachine.ChangeStateLogic(PlayerMovementStateEnums.AIM_MOVE);
    }

    public void OnStateEnter()
    {
        player.weaponCenter.FireWeapon();
    }

    public void OnStateExit()
    {
    }
}
