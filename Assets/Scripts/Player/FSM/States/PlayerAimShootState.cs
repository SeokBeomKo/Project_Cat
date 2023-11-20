using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimShootState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
        PlayerStateEnums.AIM
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
        stateMachine.ChangeStateLogic(PlayerStateEnums.AIM);
    }

    public void OnStateEnter()
    {
        player.weaponCenter.FireWeapon();
    }

    public void OnStateExit()
    {
    }
}
