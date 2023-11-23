using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaseIdleState : IPlayerState
{
    public HashSet<PlayerMovementStateEnums> allowedInputHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.CHASE_MOVE,
    };
    public HashSet<PlayerMovementStateEnums> allowedLogicHash { get; } = new HashSet<PlayerMovementStateEnums>
    {
        PlayerMovementStateEnums.CHASE_FALL,
    };

    public PlayerController player {get; set;}
    public PlayerStateMachine stateMachine {get; set;}

    public PlayerChaseIdleState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = stateMachine.playerController;
    }
    public void Execute()
    {
        if (!player.CheckGrounded())
        {
            stateMachine.ChangeStateLogic(PlayerMovementStateEnums.CHASE_FALL);
            return;
        }
    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
    }
}
