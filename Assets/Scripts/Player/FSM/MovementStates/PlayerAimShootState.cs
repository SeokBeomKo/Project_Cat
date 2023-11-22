using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimShootState : IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.AIM
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerAimShootState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }

    public void Execute()
    {
        stateMachine.ChangeStateLogic(PlayerMovementStateEnums.AIM);
    }

    public void OnStateEnter()
    {
        player.weaponCenter.FireWeapon();
    }

    public void OnStateExit()
    {
    }
}
