using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNothingShotState : IPlayerShotState
{
    public PlayerController player { get; set; }
    public PlayerShotStateMachine stateMachine { get; set; }

    public PlayerNothingShotState(PlayerShotStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
    }

    public void OnStateEnter()
    {
        player.weaponStrategy.InitShoot();
    }

    public void OnStateExit()
    {
    }
}
