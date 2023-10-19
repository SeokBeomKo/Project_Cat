using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : IPlayerState
{
    public HashSet<PlayerStateEnums> allowedInputHash { get; } = new HashSet<PlayerStateEnums>
    {
    };
    public HashSet<PlayerStateEnums> allowedLogicHash { get; } = new HashSet<PlayerStateEnums>
    {
    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerDeadState(PlayerStateMachine _stateMachine)
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
