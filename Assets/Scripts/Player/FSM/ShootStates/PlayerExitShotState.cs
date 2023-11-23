using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExitShotState : IPlayerShotState
{
    public PlayerController player { get; set; }
    public PlayerShotStateMachine stateMachine { get; set; }

    public PlayerExitShotState(PlayerShotStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        stateMachine.ChangeState(PlayerShotStateEnums.NOTHING);
        Debug.Log("Exit");
    }

    public void OnStateEnter()
    {
        player.weaponCenter.ExitShoot();
    }

    public void OnStateExit()
    {
    }
}
