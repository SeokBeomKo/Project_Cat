using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimMoveShootState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.AIM_MOVE,
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
            stateMachine.ChangeStateLogic(PlayerStateEnums.AIM);
            return;
        }

        stateMachine.ChangeStateLogic(PlayerStateEnums.AIM_MOVE);
    }

    public void OnStateEnter()
    {
        player.weaponCenter.FireWeapon();
    }

    public void OnStateExit()
    {
    }
}
