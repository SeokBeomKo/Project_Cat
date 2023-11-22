using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformState : IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {

    };
    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerTransformState(PlayerStateMachine _stateMachine)
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