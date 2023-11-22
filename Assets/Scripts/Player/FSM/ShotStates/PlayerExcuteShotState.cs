using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExcuteShotState : IPlayerShotState
{
    public PlayerController player { get; set; }
    public PlayerShotStateMachine stateMachine { get; set; }

    public PlayerExcuteShotState(PlayerShotStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
    }
}
